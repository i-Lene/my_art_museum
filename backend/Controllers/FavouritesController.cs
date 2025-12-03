
using ArtGallery.Backend.Interfaces;
using backend.Models.FavouriteArtworks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{


    [ApiController]
    [Route("api/[controller]")]


    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesService _favouritesService;

        public FavouritesController(IFavouritesService favouritesService)
        {
            _favouritesService = favouritesService;
        }


        [HttpPost]
        public async Task<IActionResult> AddToFavourites([FromBody] FavouriteArtworks favourite)
        {
            try
            {
                var result = await _favouritesService.AddToFavouritesAsync(favourite);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavouritesByUserId([FromRoute] int userId)
        {
            try
            {
                var favourites = await _favouritesService.GetFavouritesByUserIdAsync(userId);
                return Ok(favourites);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("removefavourite/{userId}/{artworkId}")]
        public async Task<IActionResult> DeleteFavouriteById([FromRoute] int userId, [FromRoute] int artworkId)
        {
            try
            {
                var result = await _favouritesService.RemoveFromFavouritesAsync(userId, artworkId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("removeallfavourites/{userId}")]
        public async Task<IActionResult> DeleteFavourites([FromRoute] int userId)
        {
            try
            {
                var result = await _favouritesService.RemoveAllFromFavouritesAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}