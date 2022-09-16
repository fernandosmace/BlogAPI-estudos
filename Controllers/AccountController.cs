using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly BlogDataContext _context;

    public AccountController(TokenService tokenService, BlogDataContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("v1/accounts/")]
    public async Task<ActionResult> Post(
        [FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-"),
            PasswordHash = PasswordHasher.Hash(model.Password)
        };

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email,
                password = user.PasswordHash
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("05X99 - Este e-mail já encontra-se cadastrado."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor."));
        }
    }

    [HttpPost("v1/login")]
    public IActionResult Login()
    {
        var token = _tokenService.GenerateToken(null);

        return Ok(token);
    }
}