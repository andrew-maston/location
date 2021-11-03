using location.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace location.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult Get([Required]Guid userId)
        {
            var user = _userRepository.Get(userId);
            return new JsonResult(user.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequest request)
        {
            var result = await _userRepository.InsertAsync(new User 
            { 
                Username = request.Username
            });

            if (result != null)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
