using location.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace location.Controllers
{
    public interface IUserRepository
    {
        Task<User> InsertAsync(User user);
        IQueryable<User> Get(Guid userId);
        IQueryable<User> GetAll();
    }
}