using AdviceMovieAPI.Context;
using AdviceMovieAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace AdviceMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private SigningCredentials signIn;
        public readonly ApplicationDBContext _context;

        public JWTTokenController(IConfiguration configuration, ApplicationDBContext context)

        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (user != null && user.UserName != null && user.Password != null)
            {
                var UserData = await GetUser(user.UserName, user.Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, Datetime.UtcNow.ToString()),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("Password", user.Password)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var SıgnIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }


                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            return BadRequest("Invalid Credentials");
        }

        [HttpGet]
        public async Task<User> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

        }
    }

    internal class Datetime
    {
        public static object UtcNow { get; internal set; }
    }

   
}


