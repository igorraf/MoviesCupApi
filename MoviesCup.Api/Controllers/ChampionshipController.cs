using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using MoviesCup.Infrastructure;

namespace MoviesCup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        private readonly MoviesCupApiContext _context;
        private readonly ChampionshipModel _chpModel;
        private readonly MovieModel _movieModel;

        public ChampionshipController(MoviesCupApiContext context)
        {
            _context = context;
            _chpModel = new ChampionshipModel(context);
            _movieModel = new MovieModel(context);
        }

        // POST: api/Movies
        [HttpPost]
        public IActionResult Championship([FromBody] List<MovieDto> movies)
        {
            var rep = _chpModel.Championship(movies).Matchs.FirstOrDefault();
            return Ok(rep);
        }

        // POST: api/Movies
        [HttpPost("ById")]
        public IActionResult Championship([FromBody] List<int> moviesId)
        {
            var rep = _chpModel.Championship(moviesId).Matchs.FirstOrDefault();
            return Ok(rep);
        }

        // POST: api/Movies
        [HttpPost("ByCupId")]
        public IActionResult ChampionshipCupId([FromBody] List<string> moviesCupId)
        {
            var rep = _chpModel.Championship(moviesCupId).Matchs.FirstOrDefault();
            return Ok(rep);
        }
    }
}