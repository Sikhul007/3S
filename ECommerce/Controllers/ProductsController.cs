using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // POST: api/products/createproduct
        [HttpPost("createproduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ProductDto>(created));
        }

        // GET: api/products/allproducts
        [HttpGet("allproducts")]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var products = await _service.GetFilteredAsync(categoryId, minPrice, maxPrice, page, limit);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }


        // GET: api/products/getbyid/5
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }


        // PUT: api/products/updateproduct/5
        [HttpPut("updateproduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            _mapper.Map(dto, product);
            var updated = await _service.UpdateAsync(product);

            return Ok(_mapper.Map<ProductDto>(updated));
        }


        // DELETE: api/products/deleteproduct/5
        [HttpDelete("deleteproduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/products/search?q=keyword
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            var results = await _service.SearchAsync(q);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(results));
        }
    }
}
