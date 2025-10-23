using befit.application.Contracts;
using befit.application.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace befit.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.GetCategories());
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("admin")]
        public async Task<IActionResult> Post([FromForm] CategoryInsertDto categoryInsertDto)
        {
            return Ok(await _categoryService.AddCategory(categoryInsertDto));
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [Route("admin")]
        public async Task<IActionResult> Put([FromForm] CategoryItemDto categoryUpdateDto)
        {
            var result = await _categoryService.UpdateCategory(categoryUpdateDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("admin/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _categoryService.DeleteCategory(id);

            return NoContent();
        }
    }
}
