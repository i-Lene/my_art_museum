using Microsoft.AspNetCore.Mvc;
using backend.Interfaces;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class MuseumDataController : ControllerBase
    {
        private readonly IArtworkService _artworkService;

        public MuseumDataController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtWorks(
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var artworks = await _artworkService.GetArtworksAsync(page, limit);
                return Ok(artworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchArtworks([FromQuery] string q, [FromQuery] int page = 1, [FromQuery] int limit = 100)
        {
            try
            {
                var artworks = await _artworkService.SearchArtworksAsync(q, page, limit);
                return Ok(artworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtWorkById(
            [FromRoute] int id, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var artworks = await _artworkService.GetArtworkAsync(id, page, limit);
                return Ok(artworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}



