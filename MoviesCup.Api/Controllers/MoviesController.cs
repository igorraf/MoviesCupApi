using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using MoviesCup.Infrastructure;

namespace MoviesCup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesCupApiContext _context;
        private readonly MovieModel _model;

        public MoviesController(MoviesCupApiContext context)
        {
            _context = context;
            _model = new MovieModel(context);
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovieDto()
        {
            return _context.MovieDto;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieDto = await _context.MovieDto.FindAsync(id);

            if (movieDto == null)
            {
                return NotFound();
            }

            return Ok(movieDto);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieDto([FromRoute] int id, [FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieDtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> PostMovieDto([FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MovieDto.Add(movieDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieDto", new { id = movieDto.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieDto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieDto = await _context.MovieDto.FindAsync(id);
            if (movieDto == null)
            {
                return NotFound();
            }

            _context.MovieDto.Remove(movieDto);
            await _context.SaveChangesAsync();

            return Ok(movieDto);
        }
        

        private bool MovieDtoExists(int id)
        {
            return _context.MovieDto.Any(e => e.Id == id);
        }
    }
}