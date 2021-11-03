using location.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace location.Controllers
{
    public class UserRepository : IUserRepository
    {
        private readonly SpatialDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(SpatialDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> InsertAsync(User user)
        {
            user.Id = Guid.NewGuid();

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred whilst inserting user", user.Username);
            }

            return user;
        }

        public IQueryable<User> Get(Guid userId)
        {
            IQueryable<User> user = null;

            try
            {
                user = _context.Users.Where(user => user.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred whilst retrieving user", userId);
            }

            return user;
        }

        public IQueryable<User> GetAll()
        {
            try
            {
                return _context.Users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred whilst retrieving users");
            }

            return null;
        }
    }
}