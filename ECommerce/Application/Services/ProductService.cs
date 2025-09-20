using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            await _repository.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            await _repository.UpdateAsync(product);
            return product;
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Product>> GetFilteredAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, int page, int limit)
        {
            return await _repository.GetFilteredAsync(categoryId, minPrice, maxPrice, page, limit);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
        {
            return await _repository.SearchAsync(keyword);
        }
    }
}
