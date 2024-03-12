using CreateApiProject.Models;
using CreateApiProject.Repositories;
using CreateApiProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CreateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly userRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public AuthController(userRepository dapperContext, ILogger<UserController> logger, IConfiguration configuration)
        {
            _repo = dapperContext;
            _logger = logger;
            _configuration = configuration;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(UserDto request)
        //{
        //    //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    user.Name = request.Username;
        //    //user.PasswordHash = passwordHash;
        //    //user.PasswordSalt = passwordSalt;

        //    return Ok(user);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = await _repo.GetUser();
                var check = users.FirstOrDefault(x => x.UserName == request.Username && x.Password == request.Password);
                if (check != null)
                {
                    string token = CreateToken(user);

                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(refreshToken);

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Tài khoản hoặc mật khẩu không Chính xác");
                }
            }
            else
            {
                return BadRequest();
            }         
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            int check = await _repo.AddUser(user);
            if (check > 0)
            {
                user.JWTToken = Constants.TOKEN_KEY;
                return Ok(user);
            }
            return BadRequest("Fail Register");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, Role.Admin)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            Constants.TOKEN_KEY = jwt.ToString();

            return jwt;
        }
        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}
