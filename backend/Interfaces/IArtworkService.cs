using backend.Models;

namespace backend.Interfaces
{
    public interface IArtworkService
    {
        Task<List<ArtworkData>> GetArtworksAsync(int limit = 10);
    }

}

