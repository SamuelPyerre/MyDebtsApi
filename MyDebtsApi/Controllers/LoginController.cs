using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDebtsApi.Data;
using MyDebtsApi.Extensions;
using MyDebtsApi.Models;
using MyDebtsApi.Services;
using MyDebtsApi.ViewModels;
using SecureIdentity.Password;

namespace MyDebtsApi.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost("v1/contas/registro")]
    public async Task<IActionResult> Post(
        [FromBody] RegistroViewModel model,
        [FromServices] MyDebtsDbContext context)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));
        }

        var usuario = new UsuarioModel
        {
            Nome = model.Nome,
            Email = model.Email
        };
        var password = PasswordGenerator.Generate(length: 16);
        usuario.Password = PasswordHasher.Hash(password);

        try
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                usuario = usuario.Email, password
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("02L01 - Este usúario já está cadastrado!"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("02L02 - Erro interno do servidor!"));
        }
    }



    [HttpPost("v1/contas/login")]
    public async Task<IActionResult> Login(
        [FromServices] TokenService tokenService,
        [FromServices] MyDebtsDbContext context,
        [FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros())); 
        }

        var usuario = await context
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        if (usuario == null)
        {
            return StatusCode(400, new ResultViewModel<string>("Usúario ou Senha inválido!"));
        }

        if (!PasswordHasher.Verify(usuario.Password, model.Password))
        {
            return StatusCode(400, new ResultViewModel<string>("Usúario ou Senha invalido!"));
        }

        try
        {
            var token = tokenService.GeradorToken(usuario);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("02L03 - Erro interno do servidor!"));
        }

        
    }
}