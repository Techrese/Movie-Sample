using AutoMapper;
using GenreApi.Controllers;
using GenreApi.Dtos;
using GenreApi.Models;
using GenreApi.Models.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace GenreApiUnitTesting
{
    public class ControllerShould
    {
        private Mock<ILogger<GenreController>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IGenreRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IGenreRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<GenreController>>();
        }

        [Test]
        public void ReturnOKWithDataWhenGetAll()
        {
            _repository.Setup(x => x.GetAllGenreAsync()).ReturnsAsync(FakeObjects.GetFakeList());

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.GetAllGenresAsync();

            int code = ((OkObjectResult)result.Result).StatusCode.Value;            

            Assert.That((int)StatusCodes.Status200OK, Is.EqualTo(code));            
        }

        [Test]
        public void ReturnBadRequestWhenExceptionThrown()
        {
            _repository.Setup(x => x.GetAllGenreAsync()).ThrowsAsync(new Exception());

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.GetAllGenresAsync();            
            
            int  code = ((BadRequestResult)result.Result).StatusCode;            

            Assert.That(code, Is.EqualTo(StatusCodes.Status400BadRequest));

        }

        [Test]
        public void ReturnNotFoundWhenNotFoundInRepository()
        {
            _repository.Setup(x => x.SearchGenre(It.IsAny<Expression<Func<Genre, bool>>>())).ReturnsAsync((Genre)null);

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.GetGenreByNameAsync("Action");

            int code = ((NotFoundResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public void ReturnOKWhenDataFoundInRepository()
        {
            _repository.Setup(x => x.SearchGenre(It.IsAny<Expression<Func<Genre, bool>>>())).ReturnsAsync(new Genre { Id = Guid.NewGuid(), Name = "Fantasy" });

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.GetGenreByNameAsync("Fantasy");

            int code = ((OkObjectResult)result.Result).StatusCode.Value;

            Assert.That(code, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void ReturnBadRequestWhenExceptionThrownInsearch()
        {
            _repository.Setup(x => x.SearchGenre(It.IsAny<Expression<Func<Genre, bool>>>())).ThrowsAsync(new Exception());

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.GetGenreByNameAsync("Fantasy");


            int code = ((BadRequestResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public void ReturnOKWhenAddData()
        {
            GenreDto genre = new()
            {  
                Name = "Action"
            };

            _repository.Setup(x => x.AddGenreAsync(It.IsAny<Genre>()));
            _repository.Setup(x => x.GetAllGenreAsync()).ReturnsAsync(FakeObjects.GetFakeList());
            _mapper.Setup(x => x.Map<Genre>(It.IsAny<GenreDto>())).Returns(new Genre() {Id = Guid.NewGuid(), Name = "Action" });

            var mockRepo = _repository.Object;

            var controller = new GenreController(mockRepo, _mapper.Object, _logger.Object);                       
            
            var result = controller.AddGenreAsync(genre);           

            int code = ((CreatedResult)result.Result).StatusCode.Value;
            

            Assert.That((int)StatusCodes.Status201Created, Is.EqualTo(code));                     

        }

        [Test]
        public void ReturnBadRequestWhenGivenInvalidDataToAdd()
        {
            GenreDto genre = new()
            {
                Name = null
            };

            _repository.Setup(x => x.AddGenreAsync(It.IsAny<Genre>()));

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.AddGenreAsync(genre);

            int code = ((BadRequestResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status400BadRequest));            
        }

        [Test]
        public void ReturnBadRequestWhenExceptionThrownInAdd()
        {
            _repository.Setup(x => x.AddGenreAsync(It.IsAny<Genre>())).Throws(new Exception());

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.AddGenreAsync(null);

            int code = ((BadRequestResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public void ReturnNotFoundWhenIdNotFound()
        {
            _repository.Setup(x => x.GetGenreAsync(It.IsAny<Guid>()));

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.DeleteGenreAsync(Guid.NewGuid());

            int code = ((NotFoundResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public void ReturnOkWhenGenreCanDelete()
        {
            _repository.Setup(x => x.GetGenreAsync(It.IsAny<Guid>())).ReturnsAsync(new Genre { Id = Guid.NewGuid(), Name = "Fantasy" });

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.DeleteGenreAsync(Guid.NewGuid());

            int code = ((OkObjectResult)result.Result).StatusCode.Value;

            Assert.That(code, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void ReturnBadRequestWhenGenreDeleteThrows()
        {
            _repository.Setup(x => x.GetGenreAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            var controller = new GenreController(_repository.Object, _mapper.Object, _logger.Object);

            var result = controller.DeleteGenreAsync(Guid.NewGuid());

            int code = ((BadRequestResult)result.Result).StatusCode;

            Assert.That(code, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}