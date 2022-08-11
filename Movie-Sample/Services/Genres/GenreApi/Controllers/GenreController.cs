using AutoMapper;
using GenreApi.Dtos;
using GenreApi.Models;
using GenreApi.Models.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GenreApi.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ExceptionFilter]
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
            _logger.LogInformation($"Get all genres from the repository.");
            return Ok(await _repository.GetAllGenreAsync());            
        }

        [HttpGet]
        [Route("Name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genre))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Genre))]        
        public async ValueTask<IActionResult> GetGenreByNameAsync(string name)
        {            
            _logger.LogInformation($"searching database for item {name}");
            var result = await _repository.SearchGenre(x => x.Name == name);
            if (result == null)
            {
                _logger.LogWarning($"Item with name: {name} was not found!");
                return NotFound();
            }                
            return Ok(result);            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Genre))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenreDto))]        
        public async ValueTask<IActionResult> AddGenreAsync(GenreDto genre)
        {           
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Given model is not valid for repository: {genre.Name}");
                return BadRequest();
            }
            Genre mappedObject = _mapper.Map<Genre>(genre);

            mappedObject.Id = Guid.NewGuid();
            _logger.LogInformation($"Genre wiht id: {mappedObject.Id} added." );
            return Created("api/v{version:apiVersion}/[controller]/{id}", await _repository.AddGenreAsync(mappedObject));            
        }

        [HttpDelete]        
        public async ValueTask<IActionResult> DeleteGenreAsync(Guid id)
        {
            
            var genre = await  _repository.GetGenreAsync(id);
            if (genre == null)
            {
                _logger.LogWarning($"Genre with id: {id} not found!");
                return NotFound();
            }
            _logger.LogInformation($"Deleting genre with id: {genre.Id}");
            _repository.DeleteGenre(genre);
            return Ok();
           
        }

        [HttpPut]        
        public IActionResult UpdateGenre(GenreDto genre)
        {
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Given model is not valid to update: {genre.Name}");
                return BadRequest();
            }
            _logger.LogInformation($"Genre with id: {genre.Name} has been updated");
            _repository.UpdateGenre(_mapper.Map<Genre>(genre));
            return Ok();
            
        }
    }
}
