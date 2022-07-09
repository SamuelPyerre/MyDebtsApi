using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyDebtsApi.Extensions;
using MyDebtsApi.Models;

namespace MyDebtsApi.Services;

public class TokenService
{
    public string GeradorToken(UsuarioModel usuario)
    {
        var tokenManipulador = new JwtSecurityTokenHandler();
        var chave = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var claims = usuario.GetClaims();
        var tokenConfiguracao = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chave),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenManipulador.CreateToken(tokenConfiguracao);
        return tokenManipulador.WriteToken(token);
    }
}