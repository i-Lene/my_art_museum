using backend.Models;

namespace backend.Interfaces
{
    public interface IArtworkService
    {
        Task<ArtworkResponse> GetArtworksAsync(int page = 1, int limit = 10);
        Task<ArtworkResponse> SearchArtworksAsync(string query, int page = 1, int limit = 100);
        Task<ArtworkData> GetArtworkAsync(int id, int page = 1, int limit = 100);
    }
}

