using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.DataAccess.Migrations;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task MarkMovieAsWatched(int userId, int movieId, bool isWatched)
        {
            var watchedMovie = new WatchedMovie { UserId = userId, MovieId = movieId, IsWatched = isWatched };
            _context.WatchedMovies.Add(watchedMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserIdExist(int userId)
        {
            return await _context.Users.AnyAsync(m => m.Id == userId);
        }

        public async Task<bool> UsernameExist(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
