using location.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using location.Models.Dtos;
using System.Threading.Tasks;

namespace location.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public LocationController(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([Required]Guid userId)
        {
            var user = _userRepository.Get(userId);

            if (!user.Any())
            {
                return NotFound($"The userId supplied {userId} is not that of a valid user.");
            }

            var location = _locationRepository.GetCurrentLocation(user);

            if (!location.Any())
            {
                return NotFound($"The supplied user {userId} does not have any previous locations");
            }

            return new JsonResult(new LocationResponse(user.FirstOrDefault(), location.FirstOrDefault()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(LocationUpdateRequest request)
        {
            var user = _userRepository.Get(request.UserId.Value);

            if (!user.Any())
            {
                return NotFound($"The userId supplied {request.UserId} is not that of a valid user.");
            }

            var result = await _locationRepository.InsertAsync(new UserLocation
            {
                Location = new Point(request.Latitude.Value, request.Longitude.Value) { SRID = 4326 },
                UserId = request.UserId.Value
            });

            if (result != null)
            {
                return Ok(new LocationResponse(user.FirstOrDefault(), result));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
