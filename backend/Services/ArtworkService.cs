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

    public async Task<List<ArtworkData>> GetArtworksAsync(int limit = 25)
    {
        var endpoint = $"{_artworksEndpoint}?limit={limit}&page=1";
        var json = await _museumApi.GetDataAsync(endpoint);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var deserialized = JsonSerializer.Deserialize<ArtworkResponse>(json, options);

        if (deserialized == null || deserialized.Data == null)
            throw new Exception("Failed to deserialize artwork data");


        foreach (var artwork in deserialized.Data)
        {
            artwork.Config = deserialized.Config;
        }

        return deserialized.Data.OrderBy(a => a.Id).ToList();
    }
}
