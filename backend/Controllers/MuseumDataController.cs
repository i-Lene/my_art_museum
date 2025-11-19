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
        public async Task<IActionResult> GetArtWorks([FromQuery] int limit = 3)
        {
            try
            {
                var artworks = await _artworkService.GetArtworksAsync(limit);
                return Ok(artworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}



