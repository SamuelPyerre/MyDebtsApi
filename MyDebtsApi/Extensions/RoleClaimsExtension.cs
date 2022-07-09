using System.Security.Claims;
using MyDebtsApi.Models;

namespace MyDebtsApi.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this UsuarioModel usuario)
    {
        var resultado = new List<Claim>
        {
            new(ClaimTypes.Name, usuario.Email),
            new(ClaimTypes.Role, "admin")
        };

        return resultado;
    }
}