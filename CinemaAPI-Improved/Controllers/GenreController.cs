using CinemaAPI_Improved.Data;
using CinemaAPI_Improved.Dtos;
using CinemaAPI_Improved.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI_Improved.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DataContext _context;

        public GenreController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
        {
            var genres = await _context.GenreList.ToListAsync();
            var genreDtos = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Type = g.Type
            }).ToList();

            return Ok(genreDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(int id)
        {
            var genre = await _context.GenreList.FindAsync(id);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }

            var genreDto = new GenreDto
            {
                Id = genre.Id,
                Type = genre.Type
            };

            return Ok(genreDto);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> AddGenre(AddGenreDto addGenreDto)
        {
            var newGenre = new Genre
            {
                Type = addGenreDto.Type
            };

            _context.GenreList.Add(newGenre);
            await _context.SaveChangesAsync();

            var genreDto = new GenreDto
            {
                Id = newGenre.Id,
                Type = newGenre.Type
            };

            return Ok(genreDto);
        }

        [HttpPut]
        public async Task<ActionResult<GenreDto>> UpdateGenre(UpdateGenreDto updateGenreDto)
        {
            var dbGenre = await _context.GenreList.FindAsync(updateGenreDto.Id);
            if (dbGenre == null)
            {
                return NotFound("Genre not found.");
            }

            dbGenre.Type = updateGenreDto.Type;

            await _context.SaveChangesAsync();

            var genreDto = new GenreDto
            {
                Id = dbGenre.Id,
                Type = dbGenre.Type
            };

            return Ok(genreDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GenreDto>>> DeleteGenre(int id)
        {
            var dbGenre = await _context.GenreList.FindAsync(id);
            if (dbGenre == null)
            {
                return NotFound("Genre not found.");
            }

            _context.GenreList.Remove(dbGenre);
            await _context.SaveChangesAsync();

            var genres = await _context.GenreList.ToListAsync();
            var genreDtos = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Type = g.Type
            }).ToList();

            return Ok(genreDtos);
        }
    }
}