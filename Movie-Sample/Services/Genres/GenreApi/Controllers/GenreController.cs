using AutoMapper;
using GenreApi.Dtos;
using GenreApi.Models;
using GenreApi.Models.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GenreApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreRepository repository, IMapper mapper, ILogger<GenreController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genre))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async ValueTask<IActionResult> GetAllGenresAsync()
        {
            try
            {
                return Ok(await _repository.GetAllGenreAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genre))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Genre))]
        public async ValueTask<IActionResult> GetGenreByNameAsync(string name)
        {
            try
            {
                var result = await _repository.SearchGenre(x => x.Name == name);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Genre))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenreDto))]
        public async ValueTask<IActionResult> AddGenreAsync(GenreDto genre)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                Genre mappedObject = _mapper.Map<Genre>(genre);

                mappedObject.Id = Guid.NewGuid();

                return Created("api/v{version:apiVersion}/[controller]/{id}", await _repository.AddGenreAsync(mappedObject));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteGenreAsync(Guid id)
        {
            try
            {
                var genre = await  _repository.GetGenreAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
                _repository.DeleteGenre(genre);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateGenre(GenreDto genre)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _repository.UpdateGenre(_mapper.Map<Genre>(genre));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
