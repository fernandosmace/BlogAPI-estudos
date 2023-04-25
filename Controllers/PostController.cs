using System.Security.Cryptography.X509Certificates;
using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [Authorize]
        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var total = await context.Posts.AsNoTracking().CountAsync();

                var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.Author)
                    .Select(x => new ListPostsViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name} ({x.Author.Email})"
                    })
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("E05x04 - Falha interna no servidor."));
            }
        }

        [HttpGet("v1/posts/{id:int}")]
        public async Task<IActionResult> DestailsAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var post = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Author)
                    .ThenInclude(x => x.Roles)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (post == null)
                    return NotFound(new ResultViewModel<Post>("Conteúdo não encontrado."));

                return Ok(new ResultViewModel<Post>(post));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("E05x04 - Falha interna no servidor."));
            }
        }

        [HttpGet("v1/posts/category/{category}")]
        public async Task<IActionResult> GetByCategoryAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] string category,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var total = await context.Posts.AsNoTracking().CountAsync();

                var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.Author)
                    .Where(x => x.Category.Slug == category)
                    .Select(x => new ListPostsViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name} ({x.Author.Email})"
                    })
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("E05x04 - Falha interna no servidor."));
            }
        }
    }
}