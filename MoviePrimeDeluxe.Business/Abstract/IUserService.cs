using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.Business.Abstract
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task MarkMovieAsWatched(int userId, int movieId, bool isWatched);
        Task<List<Movie>> GetWatchedMoviesForUser(int userId);

        Task<bool> UserIdExist(int userId);
        Task<bool> UsernameExist(string username);
    }
}
