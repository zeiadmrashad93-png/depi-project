using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using befit.application.DTOs.Categories;
using befit.domain.Contracts;
using befit.domain.Entities;

namespace befit.application.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository repository;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            repository = _unitOfWork.CategoryRepository;
        }

        public async Task<CategoryItemDto> AddCategory(CategoryInsertDto categoryInsertDto)
        {
            Category categoryEntity = new Category
            {
                Name = categoryInsertDto.Name
            };

            await repository.Create(categoryEntity);

            await _unitOfWork.SaveChanges();

            return new CategoryItemDto
            {
                Id = categoryEntity.Id,
                Name = categoryInsertDto.Name
            };
        }

        public async Task DeleteCategory(int id)
        {
            await repository.Delete(id);
        }

        public async Task<IEnumerable<CategoryItemDto>> GetCategories()
        {
            var categories = await repository.GetAll();

            return categories.Select(c => new CategoryItemDto 
            {
                Name= c.Name
            });
        }

        public async Task<CategoryItemDto?> UpdateCategory(CategoryItemDto categoryUpdateDto)
        {
            Category categoryEntity = new Category
            {
                Name = categoryUpdateDto.Name
            };

            repository.Update(categoryEntity);

            await _unitOfWork.SaveChanges();

            return new CategoryItemDto
            {
                Id = categoryEntity.Id,
                Name = categoryUpdateDto.Name
            };
        }
    }
}
