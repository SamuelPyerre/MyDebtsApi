using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyDebtsApi.Models;

namespace MyDebtsApi.Services;

public class TokenService
{
    public string GeradorToken(UsuarioModel usuario)
    {
        var tokenManipulador = new JwtSecurityTokenHandler();
        var chave = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenConfiguracao = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "administrador"),
                new (ClaimTypes.Role, "admin"),
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chave),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenManipulador.CreateToken(tokenConfiguracao);
        return tokenManipulador.WriteToken(token);
    }
}