using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using location.Models.Dtos;
using System.Collections.Generic;

namespace location.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public HistoryController(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LocationResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([Required] Guid userId)
        {
            var user = _userRepository.Get(userId);

            if (!user.Any())
            {
                return NotFound($"The userId supplied {userId} is not that of a valid user.");
            }

            var locations = _locationRepository.GetLocationHistory(user);

            if (!locations.Any())
            {
                return NotFound($"The supplied user {userId} does not have any location history");
            }

            return new JsonResult(locations.Select(l => new LocationResponse(user.FirstOrDefault(), l)));
        }
    }
}
