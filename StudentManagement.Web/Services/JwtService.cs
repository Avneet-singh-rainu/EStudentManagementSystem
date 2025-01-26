using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EStudentManagement.Web.Models;
using Microsoft.IdentityModel.Tokens;

namespace EStudentManagement.Web.Services {

    public class JwtService {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GenerateToken(User user) {

            Console.WriteLine("jwt" + user.Username + user.Role + "..............");


            var claims = new[]{
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(30));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}