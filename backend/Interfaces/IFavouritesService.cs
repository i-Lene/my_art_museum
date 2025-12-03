
using backend.Models;
using backend.Models.FavouriteArtworks;

namespace ArtGallery.Backend.Interfaces
{
    public interface IFavouritesService
    {

        Task<List<int>> AddToFavouritesAsync(FavouriteArtworks favourite);

        Task<List<ArtworkData>> GetFavouritesByUserIdAsync(int userId);

        Task<List<ArtworkData>> RemoveFromFavouritesAsync(int userId, int artworkId);

        Task<List<ArtworkData>> RemoveAllFromFavouritesAsync(int userId);
    }
}