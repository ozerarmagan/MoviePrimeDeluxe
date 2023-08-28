using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.DataAccess;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.DataAccess.Concrete;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.Business.Concrete
{
    public class UserService : IUserService
    {   
        private readonly MoviePrimeDeluxeContext _context;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, MoviePrimeDeluxeContext context) 
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<Movie>> GetWatchedMoviesForUser(int userId)
        {
            return await _userRepository.GetWatchedMoviesForUser(userId);
        }

        public async Task MarkMovieAsWatched( int userId, int movieId, bool isWatched)
        {
            var watchedMovie = new WatchedMovie {UserId = userId, MovieId = movieId, IsWatched = isWatched };
            _context.WatchedMovies.Add(watchedMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}
