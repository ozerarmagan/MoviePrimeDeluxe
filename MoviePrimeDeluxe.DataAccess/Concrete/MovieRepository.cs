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
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviePrimeDeluxeContext _context;

        public MovieRepository(MoviePrimeDeluxeContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task DeleteMovie(int id)
        {
            var deletedMovie = await GetMovieById(id);
            _context.Movies.Remove(deletedMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
