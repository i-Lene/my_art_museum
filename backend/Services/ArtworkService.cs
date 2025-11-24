using System.Text.Json;
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

    public async Task<ArtworkResponse> GetArtworksAsync(int page = 1, int limit = 10)
    {
        var endpoint = $"{_artworksEndpoint}?limit={limit}&page={page}";
        var json = await _museumApi.GetDataAsync(endpoint);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var deserialized = JsonSerializer.Deserialize<ArtworkResponse>(json, options);

        if (deserialized == null || deserialized.Data == null)
            throw new Exception("Failed to deserialize artwork data");

        foreach (var artwork in deserialized.Data)
        {
            artwork.Config = deserialized.Config;
        }

        deserialized.Data = deserialized.Data.OrderBy(a => a.Id).ToList();

        return deserialized;
    }

    public async Task<ArtworkResponse> SearchArtworksAsync(string query, int page = 1, int limit = 100)
    {
        string endpoint = $"{_artworksEndpoint}/search?q={Uri.EscapeDataString(query)}&fields=id,title,artist_display,place_of_origin,date_display,artist_titles,thumbnail,image_id,credit_line,description&limit={limit}&page={page}";

        var json = await _museumApi.GetDataAsync(endpoint);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var deserialized = JsonSerializer.Deserialize<ArtworkResponse>(json, options);

        if (deserialized == null || deserialized.Data == null)
            throw new Exception("Failed to deserialize artwork data");

        foreach (var artwork in deserialized.Data)
        {
            artwork.Config = deserialized.Config;
        }

        deserialized.Data = deserialized.Data.OrderBy(a => a.Id).ToList();

        return deserialized;
    }

    public async Task<ArtworkData> GetArtworkAsync(int id, int page = 1, int limit = 100)
    {
        string endpoint = $"{_artworksEndpoint}/{id}?limit={limit}&page={page}";

        var json = await _museumApi.GetDataAsync(endpoint);


        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        using JsonDocument doc = JsonDocument.Parse(json);
        var dataJson = doc.RootElement.GetProperty("data").GetRawText();

        var deserialized = JsonSerializer.Deserialize<ArtworkByIdResponse>(json, options);

        if (deserialized?.Data == null)
            throw new Exception("Failed to deserialize artwork data");

        deserialized.Data.Config = new Config
        {
            iiif_url = doc.RootElement.GetProperty("config").GetProperty("iiif_url").GetString()!
        };

        return deserialized.Data;
    }

}
