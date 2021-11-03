using location.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using location.Models.Dtos;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([Required]Guid userId)
        {
            var user = _userRepository.Get(userId);

            if (!user.Any())
            {
                return NotFound($"The userId supplied {userId} is not that of a valid user.");
            }

            return new JsonResult(new UserResponse(user.FirstOrDefault()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateUserRequest request)
        {
            var insertedUser = await _userRepository.InsertAsync(new User 
            { 
                Username = request.Username
            });

            if (insertedUser != null)
            {
                return Ok(new UserResponse(insertedUser));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, insertedUser);
        }
    }
}
