
using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.DataAccess
{
    public class MoviePrimeDeluxeContext : DbContext
    {
        public MoviePrimeDeluxeContext() { }
        public MoviePrimeDeluxeContext(DbContextOptions<MoviePrimeDeluxeContext> options) : base(options) 
        {
            //this.Database.Migrate();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchedMovie>()
                .HasKey(wf => new { wf.UserId, wf.MovieId });

            modelBuilder.Entity<WatchedMovie>()
                .HasOne(wf => wf.User)
                .WithMany(u => u.WatchedMovies)
                .HasForeignKey(wf => wf.UserId);

            modelBuilder.Entity<WatchedMovie>()
                .HasOne(wf => wf.Movie)
                .WithMany(f => f.WatchedMovies)
                .HasForeignKey(wf => wf.MovieId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
