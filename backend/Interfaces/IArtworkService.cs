using backend.Models;

namespace backend.Interfaces
{
    public interface IArtworkService
    {
        Task<ArtworksApiResponse> GetArtworksAsync(int page = 1, int limit = 10);
        Task<ArtworksApiResponse> SearchArtworksAsync(string query, int page = 1, int limit = 100);
        Task<ArtworkData> GetArtworkAsync(int id, int page = 1, int limit = 100);
        Task<ArtworksApiResponse> GetArtworksByArtistAsync(int artistId, int page = 1, int limit = 20);

        Task<ArtworksApiResponse> SearchArtworksByArtistAsync(int artistId, string query, int page = 1, int limit = 100);
    }
}

