using Admin.DTO;
using Admin.Models;
using Admin.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userData;
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;   
        public UserController(IUserService userService, IConfiguration config, ILogger<UserController> logger)
        {
            _userData = userService;
            _config = config;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("api/[controller]")]
        //public IActionResult GetUsers()
        //{
        //    return Ok(_UserData.GetUsers());
        //}

        //[HttpGet]
        //[Route("api/[controller]/{id}")]
        //public IActionResult GetUser(Guid id)
        //{
        //    var user=_UserData.GetUser(id);
        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }
        //    return NotFound($"User with Id: {id} was not found");
        //}

        ////[HttpPost]
        ////[Route("api/[controller]")]
        ////public IActionResult AddUser(User user)
        ////{
        ////    _UserData.AddUser(user);
        ////    return Ok();           
        ////}

        //[HttpPost]
        //[Route("api/[controller]")]
        //public async Task<User> AddUser([FromQuery] User user)
        //{
        //    return await _UserData.AddUser(user);
        //}

        //[HttpDelete]
        //[Route("api/[controller]/{id}")]
        //public IActionResult DeleteUser(Guid id)
        //{
        //    var user=_UserData.GetUser(id);
        //    if(user != null)
        //    {   
        //        _UserData.DeleteUser(user);
        //        return Ok();
        //    }

        //    return NotFound($"User with Id: {id} was not found");
        //}

        //[HttpPut]
        //[Route("api/[controller]/{id}")]
        //public IActionResult EditUser(Guid id, User user)
        //{
        //    var existinguser=_UserData.GetUser(id);
        //    if( existinguser != null)
        //    {
        //        user.ID = existinguser.ID;
        //        _UserData.EditUser(user);
        //        return Ok();
        //    }

        //    return NotFound($"User with Id: {id} was not found");
        //}


        [HttpGet("LogIn")]
        public async Task<string> Login(string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                }),

                Expires = DateTime.UtcNow.AddMinutes(18),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }




        [HttpGet]
        public async Task<List<User>> GetAllUser()
        {
            return await _userData.GetAllUser();
        }

        [HttpGet("{id}")]
        public async Task<List<User>> GetAllUser(Guid id)
        {
            return await _userData.GetAllUser(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<User> AddUser([FromQuery] User user)
        {
          User user1 =  await _userData.AddUser(user);
            _logger.LogInformation($"{user.Email}");
            return user1;
        }

        [Authorize]
        [HttpPut]
        public async Task<UserDTO> UpdateUser([FromQuery] UserDTO userDTO, Guid id)
        {
            return await _userData.UpdateUser(userDTO, id);
        }

        [Authorize]
        [HttpDelete]
        public async Task<User> DeleteUser(Guid id)
        {
            return await _userData.DeleteUser(id);
        }

    }
}
