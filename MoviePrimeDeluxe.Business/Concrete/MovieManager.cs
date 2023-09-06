using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (await MovieNameExist(null,movie.Name))
            {
                throw new Exception("A movie with this name already exists.");
            }
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
            if (await MovieIdExist(id))
            {
                return await _movieRepository.GetMovieById(id);   
            }
            throw new Exception("A movie with this id not exists.");
        }

        public async Task<Movie> GetMovieByName(string name)
        {            
            return await _movieRepository.GetMovieByName(name);            
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
           
            if (await MovieNameExist(movie.Id,movie.Name))
            {
                throw new Exception("You can not update to an existing name.");
            }
            return await _movieRepository.UpdateMovie(movie);
        }

        public async Task<bool> MovieIdExist(int movieId)
        {
            return await _movieRepository.MovieIdExist(movieId);
        }

        public async Task<bool> MovieNameExist(int? movieId, string name)
        {
            return await _movieRepository.MovieNameExist(movieId, name);
        }

    }
}
