using location.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;

namespace location.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;

        public SearchController(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchResult>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Search(SearchRequest request)
        {
            var searchResults = new List<SearchResult>();

            try
            {
                var users = _userRepository.GetAll();
                var currentLocations = _locationRepository.GetCurrentLocation(users);
                var searchLocation = new Point(request.Latitude, request.Longitude) { SRID = 4326 };
                searchResults = currentLocations.Where(t => t.Location.Distance(searchLocation) > request.SearchArea)
                    .Select(userLocation => new SearchResult(userLocation.User, userLocation, userLocation.Location.Distance(searchLocation)))
                    .ToList();
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, request);
            }

            return new JsonResult(searchResults);
        }
    }
}
