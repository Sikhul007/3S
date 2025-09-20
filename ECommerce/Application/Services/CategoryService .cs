using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            if (await _repository.ExistsByNameAsync(category.Name))
                throw new Exception("Category name already exists");

            await _repository.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.HasProductsAsync(id))
                throw new Exception("Cannot delete category with products");

            await _repository.DeleteAsync(id);
        }
    }
}
