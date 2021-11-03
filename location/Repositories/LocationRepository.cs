using location.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace location.Controllers
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SpatialDbContext _context;
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(SpatialDbContext context, ILogger<LocationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<UserLocation> GetCurrentLocation(IQueryable<User> user)
        {
            IQueryable<UserLocation> currentLocation = null;

            try
            {
               currentLocation = user
                  .Include(user => user.Locations)
                  .SelectMany(user => user.Locations.OrderByDescending(l => l.Timestamp).Take(1));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred whilst retrieving current location", user.FirstOrDefault().Id, user.FirstOrDefault().Username);
            }

            return currentLocation;
        }

        public async Task<UserLocation> InsertAsync(UserLocation location)
        {
            location.Timestamp = DateTime.UtcNow;
            location.Id = Guid.NewGuid();

            try
            {
                _context.UserLocations.Add(location);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred whilst inserting user location", location.UserId);
            }

            return location;
        }
    }
}