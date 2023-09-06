using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.DataAccess;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.DataAccess.Concrete;
using MoviePrimeDeluxe.DataAccess.Migrations;
using MoviePrimeDeluxe.Entities;
using System.Security.Claims;

namespace MoviePrimeDeluxe.Business.Concrete
{
    public class UserService : IUserService
    {   
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        public UserService(IUserRepository userRepository, IMovieRepository movieRepository) 
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }
        public async Task<User> GetUserById(int id)
        {
            if (await UserIdExist(id))
            {
                return await _userRepository.GetUserById(id);
            }
            throw new Exception("An user with this id is not exists.");
        }

        public async Task<List<Movie>> GetWatchedMoviesForUser(int userId)
        {
            if(await UserIdExist(userId))
            {
                var movies = await _userRepository.GetWatchedMoviesForUser(userId);

                if(movies.Count == 0)
                {
                    throw new Exception("This user hasn't watched any movies yet.");
                }

                movies.ForEach(x => x.WatchedMovies = null);
                return movies;
            }
            throw new Exception("An user with this id is not exists.");
        }


        public async Task MarkMovieAsWatched(int userId, int movieId, bool isWatched)
        {         
            if(await UserIdExist(userId) && await MovieIdExist(movieId))
            {
                await _userRepository.MarkMovieAsWatched(userId, movieId, isWatched);
            }
            else if((!await UserIdExist(userId) && await MovieIdExist(movieId)) || (await UserIdExist(userId) && !await MovieIdExist(movieId)))
            {
                throw new Exception("User id or movie id is false.");
            }          
        }

        public async Task<bool> MovieIdExist(int movieId)
        {
            return await _movieRepository.MovieIdExist(movieId);
        }

        public async Task<bool> UserIdExist(int userId)
        {
            return await _userRepository.UserIdExist(userId);
        }

        public async Task<bool> UsernameExist(string username)
        {
            return await _userRepository.UsernameExist(username);
        }
    }
}
