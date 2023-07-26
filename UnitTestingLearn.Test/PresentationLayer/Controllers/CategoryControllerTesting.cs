using AutoFixture;
using AutoMapper;
using ECommerceAPI.BusinessLayer.IHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTestingLearn.BuisnessLayer;
using UnitTestingLearn.DataLayer.Dtos;
using UnitTestingLearn.DataLayer.Models;
using UnitTestingLearn.DataLayer.Repositories.Interface;
using UnitTestingLearn.PresentationLayer.Controllers;
using Xunit;

namespace UnitTestingLearn.Test.PresentationLayer.Controllers
{
    public class CategoryControllerTesting
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBaseRepository<Category>> _mockRepository;
        private readonly Mock<IImageService> _mockImageService;
        private readonly IMapper _mapper;
        private readonly CategoryController _controller;

        public CategoryControllerTesting()
        {
            // for creating dummy objects
            _fixture = new Fixture();
            // for creating mocking services
            _mockRepository = new Mock<IBaseRepository<Category>>();
            _mockImageService = new Mock<IImageService>();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            _controller = new CategoryController(_mockRepository.Object, _mockImageService.Object, _mapper);
        }
        [Fact]
        public void GetAll_Categorry_ShouldReturnListOfData()
        {
            // Arrange - prepare data
            // dummy returning data
            var mockData = _fixture.Create<IEnumerable<Category>>();
            // setup mock service method
            _mockRepository.Setup(x => x.GetAll()).Returns(mockData);

            // Act - execute the testable method
            var result = _controller.GetAll();

            // Assert
            result.Should().NotBeNull();
            // check the return type of result
            result.Should().BeAssignableTo<IEnumerable<Category>>();
            // check that the data is the same as dummy generated data
            result.Should().HaveCount(mockData.Count());
            result.Should().BeEquivalentTo(mockData);
            // verify that the service has been called once
            _mockRepository.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task Get_Categorry_ShouldReturnOkResult_WhenDataFound()
        {
            // Arrange - prepare data
            // dummy returning data
            var mockData = _fixture.Create<Category>();
            var id = _fixture.Create<long>();
            // setup mock service method
            _mockRepository.Setup(x => x.GetById(id)).Returns(mockData);

            // Act - execute the testable method
            var result = await _controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            // check the return type of result
            result.Should().BeAssignableTo<ActionResult<Category>>();
            // check that the status code is correct
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            // check the content of the response is correct
            result.Result.As<OkObjectResult>().Value
                .Should().NotBeNull()
                .And.BeOfType(mockData.GetType())
                .And.BeEquivalentTo(mockData);
            // verify that the service has been called once
            _mockRepository.Verify(x => x.GetById(id), Times.Once());
        }

        [Fact]
        public async Task Get_Categorry_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange - prepare data
            // dummy returning data
            Category mockData = null;
            var id = _fixture.Create<long>();
            // setup mock service method
            _mockRepository.Setup(x => x.GetById(id)).Returns(mockData);

            // Act - execute the testable method
            var result = await _controller.Get(id);

            // Assert
            // check that the response is not null
            result.Should().NotBeNull();
            // check the return type of result
            result.Should().BeAssignableTo<ActionResult<Category>>();
            // check that the status code is correct
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            // verify that the service has been called once
            _mockRepository.Verify(x => x.GetById(id), Times.Once());
        }

        [Fact]
        public async Task Post_Categorry_ShouldReturnOkResult_WhenDataIsValid()
        {
            // Arrange - prepare data
            // dummy returning data
            var mockImage = new Mock<IFormFile>();
            mockImage.Setup(x => x.FileName).Returns("dummy.jpg");
            mockImage.Setup(x => x.Length).Returns(1000);
            mockImage.Setup(x => x.ContentType).Returns("image/jpeg");

            CategoryAddDto mockData = new CategoryAddDto
            {
                Name = "Test",
                Image = mockImage.Object
            };
            
            Category resultData = _mapper.Map<Category>(mockData);
            resultData.Image = "Images//" + mockImage.Object.FileName;

            _mockImageService.Setup(x => x.SaveImage(It.IsAny<IFormFile>()))
                .Returns("Images//" + mockImage.Object.FileName);

            // Act - execute the testable method
            var result = await _controller.Post(mockData);

            // Assert
            // check that the response is not null
            result.Should().NotBeNull();
            // check the return type of result
            result.Should().BeAssignableTo<ActionResult<Category>>();
            // check that the status code is correct
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            // check the content of the response is correct
            result.Result.As<OkObjectResult>().Value
                .Should().NotBeNull()
                .And.BeOfType(resultData.GetType())
                .And.BeEquivalentTo(resultData);
            // verify that the service has been called once and the paramter data is correct
            _mockImageService.Verify(x => x.SaveImage(It.Is<IFormFile>(x => x.Equals(mockData.Image))), 
                Times.Once());
            _mockRepository.Verify(x => x.Create(It.IsAny<Category>(/*x => x.Equals(resultData)*/)),
                Times.Once());
        }

        [Fact]
        public async Task Post_Categorry_ShouldReturnBadRequest_WhenImageIsNull()
        {
            // Arrange - prepare data
            CategoryAddDto mockData = new CategoryAddDto
            {
                Name = "Test",
                Image = null
            };

            Category resultData = _mapper.Map<Category>(mockData);

            // Act - execute the testable method
            var result = await _controller.Post(mockData);

            // Assert
            // check that the response is not null
            result.Should().NotBeNull();
            // check the return type of result
            result.Should().BeAssignableTo<ActionResult<Category>>();
            // check that the status code is correct
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            // check the content of the response is correct
            result.Result.As<BadRequestObjectResult>().Value
                .Should().NotBeNull()
                .And.BeOfType<string>()
                .And.BeEquivalentTo("Image Is Required");
            // verify that the service has been called once and the paramter data is correct
            _mockImageService.Verify(x => x.SaveImage(It.IsAny<IFormFile>()), Times.Never());
            _mockRepository.Verify(x => x.Create(It.IsAny<Category>()), Times.Never());
        }
    }
}
