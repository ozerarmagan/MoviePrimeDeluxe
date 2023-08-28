using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<Movie> CreateMovie(Movie movie)
        {
            return await _movieRepository.CreateMovie(movie);
        }

        public async Task DeleteMovie(int id)
        {
            await _movieRepository.DeleteMovie(id);
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _movieRepository.GetAllMovies();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _movieRepository.GetMovieById(id);
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            return await _movieRepository.GetMovieByName(name);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            return await _movieRepository.UpdateMovie(movie);
        }
    }
}
