using Microsoft.AspNetCore.Mvc;
using MoviesCup.Api.Controllers;
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesCupApiTest
{
    public class UnitTestChampionship
    {
        MoviesController _moviesController = null;
        ChampionshipController _championshipController = null;

        public UnitTestChampionship()
        {
            MoviesCupApiContext.UrlMoviesSource = "https://copadosfilmes.azurewebsites.net/api/filmes";
            var context = DbContextFactory<MoviesCupApiContext>.Memory();
            _moviesController = new MoviesController(context);
            _championshipController = new ChampionshipController(context);
        }

        [Fact]
        public void ChampApi4Movies()
        {
            var list = new List<string> { "tt7784604", "tt4881806", "tt3778644", "tt5463162" };

            var result = _championshipController.ChampionshipCupId(list);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okObjectResult.StatusCode);
            var champ = okObjectResult.Value as MatchDto;

            Assert.Equal("tt5463162", champ.First.CupId);
            Assert.Equal("tt3778644", champ.Second.CupId);
        }

        [Fact]
        public void ChampApi8Movies()
        {
            var list = new List<string> {
                "tt1825683", "tt3501632", "tt1365519", "tt7784604",
                "tt6499752", "tt3799232", "tt3606756", "tt5463162" };

            var result = _championshipController.ChampionshipCupId(list);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okObjectResult.StatusCode);
            var champ = okObjectResult.Value as MatchDto;

            Console.WriteLine(champ);

            Assert.Equal("tt3606756", champ.First.CupId);
            Assert.Equal("tt3501632", champ.Second.CupId);
        }
    }
}
