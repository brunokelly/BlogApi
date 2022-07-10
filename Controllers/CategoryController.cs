using BlogApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categoires")]
        public async Task<IActionResult> GetAsync([FromServices] BlogApiDataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return Ok(categories);
        }
    }
}
