using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(User user);
        Task<List<Movie>> GetWatchedMoviesForUser(int userId);
    }
}
