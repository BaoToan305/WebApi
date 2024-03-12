using CreateApiProject.Models;
using CreateApiProject.Repositories;
using CreateApiProject.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly userRepository _repo;
        private readonly ILogger<UserController> _logger;

        public UserController(userRepository dapperContext, ILogger<UserController> logger)
        {
            _repo = dapperContext;
            _logger = logger;
        }
        
        [HttpGet("get-users"), Authorize(Roles = "Admin")]
        
        public async Task<ActionResult<List<User>>> GetUser()
        {
            IEnumerable<User> users = await _repo.GetUser();
            if(users != null)
            {
                foreach(User user in users)
                {
                    user.JWTToken = Constants.TOKEN_KEY;
                }
                return Ok(users);
            }
            return BadRequest();
        }

    }
}
