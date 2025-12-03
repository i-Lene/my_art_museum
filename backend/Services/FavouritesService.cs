using ArtGallery.Backend.Interfaces;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using backend.Models.FavouriteArtworks;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class FavouritesService : IFavouritesService
{
    private readonly AppDbContext _dbContext;
    private readonly IArtworkService _artworkService;

    public FavouritesService(IArtworkService artworkService, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _artworkService = artworkService;
    }

    public async Task<List<int>> AddToFavouritesAsync(FavouriteArtworks favourite)
    {
        var exists = await _dbContext.favourites
            .AnyAsync(f => f.userId == favourite.UserId && f.artworkId == favourite.ArtworkId);

        if (!exists)
        {
            var fav = new Favourites
            {
                userId = favourite.UserId,
                artworkId = favourite.ArtworkId
            };
            await _dbContext.favourites.AddAsync(fav);
            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.favourites
            .Where(f => f.userId == favourite.UserId)
            .Select(f => f.artworkId)
            .ToListAsync();
    }


    public async Task<List<ArtworkData>> GetFavouritesByUserIdAsync(int userId)
    {
        var favouriteArtworkIds = await _dbContext.favourites
            .Where(f => f.userId == userId)
            .Select(f => f.artworkId)
            .ToListAsync();

        var favouriteArtworks = new List<ArtworkData>();

        foreach (var artworkId in favouriteArtworkIds)
        {
            try
            {
                var artwork = await _artworkService.GetArtworkAsync(artworkId);

                favouriteArtworks.Add(artwork);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching artwork with ID {artworkId}: {ex.Message}");
            }
        }

        return favouriteArtworks;
    }


    public async Task<List<ArtworkData>> RemoveFromFavouritesAsync(int userId, int artworkId)
    {
        var favourite = await _dbContext.favourites
            .FirstOrDefaultAsync(f => f.userId == userId && f.artworkId == artworkId);

        if (favourite != null)
        {
            _dbContext.favourites.Remove(favourite);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Favourite not found");
        }

        return await GetFavouritesByUserIdAsync(userId);
    }

    public async Task<List<ArtworkData>> RemoveAllFromFavouritesAsync(int userId)
    {
        var favourites = await _dbContext.favourites
            .Where(f => f.userId == userId)
            .ToListAsync();

        _dbContext.favourites.RemoveRange(favourites);
        await _dbContext.SaveChangesAsync();

        return new List<ArtworkData>();
    }

}