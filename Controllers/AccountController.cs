using BlogApi.Data;
using BlogApi.Extensions;
using BlogApi.Models;
using BlogApi.Services;
using BlogApi.ViewModels;
using BlogApi.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace BlogApi.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel model,
        [FromServices] EmailService emailService,
            [FromServices] BlogApiDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Slug = model.Email.Replace("@", "-").Replace(".", "-"),
                GitHub = "https://github.com/brunokelly"
            };

            var password = PasswordGenerator.Generate(25);
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                emailService.Send(user.Name, user.Email, "Bem vindo", body: $"Sua senha é {password}");

                return Ok(new ResultViewModel<dynamic>(data: new
                {
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("01EX13 - Este e-mail já está cadastrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("01EX14 - Falha interna no servidor"));

            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, [FromServices] BlogApiDataContext context
            , [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<Category>("Email ou senha invalida."));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            {
                return StatusCode(401, new ResultViewModel<Category>("Email ou senha invalida."));
            }

            try
            {
                var token = tokenService.GenerateToken(user);

                return Ok(new ResultViewModel<string>(token, null));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
