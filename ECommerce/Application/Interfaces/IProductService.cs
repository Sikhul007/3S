using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetFilteredAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, int page, int limit);
        Task<IEnumerable<Product>> SearchAsync(string keyword);
    }
}
