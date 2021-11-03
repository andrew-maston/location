using location.Models;
using System.Linq;
using System.Threading.Tasks;

namespace location.Controllers
{
    public interface ILocationRepository
    {
        Task<UserLocation> InsertAsync(UserLocation location);
        IQueryable<UserLocation> GetCurrentLocation(IQueryable<User> user);
        IQueryable<UserLocation> GetLocationHistory(IQueryable<User> user);
    }
}