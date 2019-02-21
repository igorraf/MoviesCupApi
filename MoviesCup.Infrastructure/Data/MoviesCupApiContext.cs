using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using MoviesCup.Domain;
using MoviesCup.Infrastructure;

namespace MoviesCup.Api.Models
{
    public class MoviesCupApiContext : DbContext
    {
        public static string UrlMoviesSource { get; set; }

        public MoviesCupApiContext (DbContextOptions<MoviesCupApiContext> options, string moviesUrlSource = ""): base(options)
        {
            UrlMoviesSource = (!String.IsNullOrEmpty(moviesUrlSource)) ? moviesUrlSource : UrlMoviesSource;
            if (options.FindExtension<InMemoryOptionsExtension>() != null)
            {
                MovieModel model = new MovieModel(this);
                model.Init(UrlMoviesSource);
            }
        }

        public DbSet<MoviesCup.Domain.MovieDto> MovieDto { get; set; }
    }
}
