using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCup.Api.Controllers;
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MoviesCupApiTest
{
    public class UnitTestMovies
    {
        MoviesController _moviesController = null;

        public UnitTestMovies()
        {
            MoviesCupApiContext.UrlMoviesSource = "https://copadosfilmes.azurewebsites.net/api/filmes";
            _moviesController = new MoviesController(DbContextFactory<MoviesCupApiContext>.Memory());
        }

        [Fact]
        public async Task MoviesApiCreateAsync()
        {           
            MovieDto movie = new MovieDto();
            movie.Id = -1;
            movie.CupId = "Teste01";
            movie.CupRate = 1.5;
            movie.Title = "Teste 01";
            movie.Year = 2019;

            var result = await _moviesController.PostMovieDto(movie);

            var okObjectResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task MoviesApiEditAsync()
        {
            MovieDto movie = new MovieDto();
            movie.Id = -1;
            movie.Title = "Teste 01 - Edit";
            movie.Year = 2001;

            var resultPut = await _moviesController.PutMovieDto(-1, movie);

            var noContentResult = Assert.IsType<NoContentResult>(resultPut);

            Assert.Equal(204, noContentResult.StatusCode);

            var resultGet = await _moviesController.GetMovieDto(movie.Id);
            var okObjectResult = Assert.IsType<OkObjectResult>(resultGet);

            var objE = okObjectResult.Value as MovieDto;

            Assert.Equal(movie.Title, objE.Title);
            Assert.Equal(movie.Year, objE.Year);
        }

        [Fact]
        public async Task MoviesApiDeleteAsync()
        {
            var result = await _moviesController.DeleteMovieDto(-1);

            var noContentResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, noContentResult.StatusCode);
        }
    }
}
