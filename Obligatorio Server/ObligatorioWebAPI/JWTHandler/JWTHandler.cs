using DTOs.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObligatorioWebAPI.JWTHandler
{
    public class JWTHandler
    {
        public static string GenerarToken(UsuarioDTO usuarioDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //clave secreta, generalmente se incluye en el archivo de configuración
            //Debe ser un vector de bytes 

            var clave = Encoding.ASCII.GetBytes("obligatoriosecrettokenkeyFedericoClavijo");

            //Se incluye un claim para el rol

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarioDto.User),
                    new Claim(ClaimTypes.Role, usuarioDto.Rol)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
