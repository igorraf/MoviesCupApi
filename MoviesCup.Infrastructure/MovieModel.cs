
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using MoviesCup.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Extensions;

namespace MoviesCup.Infrastructure
{
    public class MovieModel
    {
        public MoviesCupApiContext _context { get; set; }

        public MovieModel(MoviesCupApiContext context)
        {
            _context = context;
        }
        
        
        internal void Init(string url)
        {
            if (_context.MovieDto.Count() > 0)
            {
                Console.WriteLine("Its Loaded");
                return;
            }

            Task<List<MovieJsonDto>> taskLoadMovies = ApiTools.GetList<MovieJsonDto>(url);
            taskLoadMovies.Wait();

            List<MovieDto> movies = taskLoadMovies.Result.ToDto();

            _context.AddRange(movies);
            _context.SaveChanges();
        }

        

        
    }
}
