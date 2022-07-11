using BlogApi.Data;
using BlogApi.Models;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogApiDataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] BlogApiDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogApiDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categoires/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "01XE9 - Não foi possivel incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "01XE10 - Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
          [FromRoute] int id,
          [FromBody] EditorCategoryViewModel model,
          [FromServices] BlogApiDataContext context)
        {
            try
            {
                var category = await context
                                      .Categories
                                      .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "01XE11 - Não foi possivel incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "01EX12 Falha interna no servidor");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
          [FromRoute] int id,
          [FromServices] BlogApiDataContext context)
        {
            try
            {
                var category = await context
                                      .Categories
                                      .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "01EX13 - Não foi possivel incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "01EX14 - Falha interna no servidor");
            }

        }
    }
}
