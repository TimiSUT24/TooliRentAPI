using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TooliRentAPI.Services.Interfaces;

namespace TooliRentAPI.Services
{
    public class JwtService : IJwt
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration; //access to appsettings.json 
        }
        public string GenerateToken(string userId, IList<string> roles)
        {
            // Information about the user in the token 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userId!),
                new Claim(ClaimTypes.Email, userId!)
            };

            //Add roles to the claims 
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Creates a security key from the secret key in appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // HMAC SHA256 algorithm for signing the token

            //Create token object 
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT:ExpireInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
