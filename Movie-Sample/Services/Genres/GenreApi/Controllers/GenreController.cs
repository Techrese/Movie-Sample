using GenreApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenreApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllGenres()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetGenreByName(string name)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddGenre(GenreDto genre)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteGenre(Guid id)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateGenre(GenreDto genre)
        {
            return Ok();
        }
    }
}
