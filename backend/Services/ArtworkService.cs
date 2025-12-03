using System.Text.Json;
using backend.Data;
using backend.Interfaces;
using backend.Models;


namespace backend.Services;

public class ArtworkService : IArtworkService
{
    private readonly IMuseumApiService _museumApi;
    private readonly string _artworksEndpoint;

    public ArtworkService(IMuseumApiService museumApi, IConfiguration configuration)
    {
        _museumApi = museumApi;
        _artworksEndpoint = configuration["ApiSettings:ArtworksEndpoint"]!;
    }

    public async Task<ArtworksApiResponse> GetArtworksAsync(int page = 1, int limit = 10)
    {
        var endpoint = $"{_artworksEndpoint}?limit={limit}&page={page}";
        var json = await _museumApi.GetDataAsync(endpoint);

        return ProcessArtworksResponseAsync(json);
    }

    public async Task<ArtworksApiResponse> SearchArtworksAsync(string query, int page = 1, int limit = 100)
    {
        string endpoint = $"{_artworksEndpoint}/search?q={Uri.EscapeDataString(query)}&fields=id,title,artist_display,place_of_origin,date_display,artist_titles,thumbnail,image_id,credit_line,description&limit={limit}&page={page}";

        var json = await _museumApi.GetDataAsync(endpoint);

        return ProcessArtworksResponseAsync(json);
    }

    public async Task<ArtworkData> GetArtworkAsync(int id, int page = 1, int limit = 100)
    {
        string endpoint = $"{_artworksEndpoint}/{id}?limit={limit}&page={page}";

        string json = await _museumApi.GetDataAsync(endpoint);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var result = JsonSerializer.Deserialize<ArtworkByIdResponse>(json, options)
                     ?? throw new Exception("Failed to deserialize artwork data");

        if (result.Data == null)
            throw new Exception("Artwork data is null");

        result.Data.Config = result.Config;

        return result.Data;
    }

    public async Task<ArtworksApiResponse> GetArtworksByArtistAsync(int artistId, int page = 1, int limit = 20)
    {
        string endpoint =
            $"{_artworksEndpoint}/search?" +
            $"&query[term][artist_ids]={artistId}" +
            $"&fields=id,title,artist_display,artist_titles,thumbnail,image_id,place_of_origin,date_display,credit_line,description" +
            $"&limit={limit}&page={page}";


        var json = await _museumApi.GetDataAsync(endpoint);

        return ProcessArtworksResponseAsync(json);
    }

    public async Task<ArtworksApiResponse> SearchArtworksByArtistAsync(int artistId, string query, int page = 1, int limit = 100)
    {
        string endpoint =
            $"{_artworksEndpoint}/search?" +
            $"q={Uri.EscapeDataString(query)}" +
            $"&query[term][artist_ids]={artistId}" +
            $"&fields=id,title,artist_display,artist_titles,thumbnail,image_id,place_of_origin,date_display,credit_line,description" +
            $"&limit={limit}&page={page}";

        var json = await _museumApi.GetDataAsync(endpoint);

        return ProcessArtworksResponseAsync(json);
    }

    private ArtworksApiResponse ProcessArtworksResponseAsync(string json)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var deserialized = JsonSerializer.Deserialize<ArtworksApiResponse>(json, options);

        if (deserialized == null || deserialized.Data == null)
            throw new Exception("Failed to deserialize artwork data");

        foreach (var artwork in deserialized.Data)
        {
            artwork.Config = deserialized.Config;
        }

        deserialized.Data = deserialized.Data.OrderBy(a => a.Id).ToList();

        return deserialized;
    }

}