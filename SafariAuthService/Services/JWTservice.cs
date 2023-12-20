using Ecommerce.Services.Iservices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SafariAuthService.Models;
using SafariAuthService.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services
{
    public class JWTservice : IJwt
    {
        private readonly JWToptions _jwtoptions;
        public JWTservice(IOptions<JWToptions> options)
        {

            _jwtoptions = options.Value;


            
        }
        public string GetToken(ApplicationUser user , IEnumerable<string> Roles)
        {
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtoptions.SecretKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Adding the payload 

            List<Claim> Claims = new List<Claim>();
           
            Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));


            // Adding a list of roles into our payload 
            Claims.AddRange(Roles.Select(x => new Claim(ClaimTypes.Role, x)));


            // Combining everything together [credentials , claims , Issuer and Audience]

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtoptions.Issuer,
                Audience = _jwtoptions.Audience,
                SigningCredentials = cred,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(Claims)

            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokendescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
