using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> HasProductsAsync(int categoryId);
    }
}
