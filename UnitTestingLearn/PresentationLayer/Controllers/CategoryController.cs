using AutoMapper;
using ECommerceAPI.BusinessLayer.IHelpers;
using Microsoft.AspNetCore.Mvc;
using UnitTestingLearn.DataLayer.Dtos;
using UnitTestingLearn.DataLayer.Models;
using UnitTestingLearn.DataLayer.Repositories.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitTestingLearn.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public CategoryController(IBaseRepository<Category> repository,
            IImageService imageService, IMapper mapper)
        {
            _repository = repository;
            _imageService = imageService;
            _mapper = mapper;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = _repository.GetById(id);
            if (category != null)
                return Ok(category);
            
            return NotFound();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CategoryAddDto dto)
        {
            if (dto.Image == null)
                return BadRequest("Image Is Required");

            _imageService.SaveImage(dto.Image);

            var category = _mapper.Map<Category>(dto);
            _repository.Create(category);

            return Ok(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
