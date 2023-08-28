using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviePrimeDeluxeContext _context;
        public UserRepository(MoviePrimeDeluxeContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<Movie>> GetWatchedMoviesForUser(int userId)
        {
            var watchedMovies = await _context.WatchedMovies
            .Where(wm => wm.UserId == userId && wm.IsWatched)
            .Select(wm => wm.Movie)
            .ToListAsync();

            return watchedMovies;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
