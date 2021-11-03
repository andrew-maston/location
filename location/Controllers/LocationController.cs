using location.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace location.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly SpatialDbContext _spatialDbContext;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LocationController> _logger;

        public LocationController(SpatialDbContext spatialDbContext, IUserRepository userRepository, ILocationRepository locationRepository,  ILogger<LocationController> logger)
        {
            _spatialDbContext = spatialDbContext;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }


        [HttpGet]
        public IActionResult Get([Required]Guid userId)
        {
            var user = _userRepository.Get(userId);
            var location = _locationRepository.GetCurrentLocation(user);
            return new JsonResult(new LocationResponse(user.FirstOrDefault(), location.FirstOrDefault()));
        }

        [HttpPost]
        public IActionResult Post(LocationUpdateRequest request)
        {
            var user = _userRepository.Get(request.UserId.Value);

            if (!user.Any())
            {
                return NotFound($"The userId supplied {request.UserId} is not that of a valid user.");
            }

            var result = _locationRepository.InsertAsync(new UserLocation
            {
                Location = new Point(request.Latitude.Value, request.Longitude.Value) { SRID = 4326 },
                UserId = request.UserId.Value
            });

            if (result != null)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

   //     [HttpPost]
   //     public async Task<IActionResult> Search([FromForm] SearchRequest request)
   //     {
   //         var location = new Point(request.Latitude, request.Longitude) { SRID = 4326 };

   //         var userLocations = _spatialDbContext.Users
   //             .Include(user => user.Locations)
   //             .SelectMany(user => user.Locations.OrderByDescending(l => l.Timestamp).Take(1),


   //             (user, loc) => new { user.Username, user.Id, Latitude = loc.Location.Y, Longitude = loc.Location.X, Timestamp = loc.Timestamp })
   //             .Where(t => t.Distance(location) > 1)

   //             .ToList();
                
                
   //             //UserLocations
   //             //.SelectMany(user => user.)           //     .UserLocations.Where(t => t.Location.Distance(location) > 1)
   //             //.Select(t => new { t.UserId, Latitude = t.Location.Y, Longitude = t.Location.X, t.Timestamp })
   //             //.ToList();


   ////         context.Topic
   ////.SelectMany(topic => topic.Posts.OrderByDescending(z => z.CreatedDate).Take(1),
   ////     (topic, post) => new { topic.Id, topic.Title, post.Text, post.CreatedDate })
   ////.OrderByDescending(x => x.CreatedDate)
   ////.ToList();

   //         return new JsonResult(userLocations);
   //     }
    }
}
