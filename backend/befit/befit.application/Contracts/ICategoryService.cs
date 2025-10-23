using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.DTOs.Categories;

namespace befit.application.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryItemDto>> GetCategories();

        Task<CategoryItemDto> AddCategory(CategoryInsertDto categoryInsertDto);

        Task<CategoryItemDto?> UpdateCategory(CategoryItemDto categoryUpdateDto);

        Task DeleteCategory(int id);
    }
}
