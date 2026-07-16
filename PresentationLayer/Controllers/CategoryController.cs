using BusinessLogic;
using BusinessLogic.Interfaces;
using Domain.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPUserApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var id = await _categoryService.Create(createCategoryDTO);
            return CreatedAtAction(nameof(GetCategoryById),new { id},id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize,1,100);

            var categories = await _categoryService.GetAllPaged(pageNumber, pageSize);

            return Ok(categories);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.Update(id, updateCategoryDTO);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
