using CinemaAPI_Improved.Data;
using CinemaAPI_Improved.Dtos;
using CinemaAPI_Improved.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI_Improved.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DataContext _context;

        public MovieController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetAllMovies()
        {
            var movies = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .ToListAsync();

            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                DirectorName = $"{m.DDetails.FirstName} {m.DDetails.LastName}",
                GenreType = m.GDetails.Type
            }).ToList();

            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                DirectorName = $"{movie.DDetails.FirstName} {movie.DDetails.LastName}",
                GenreType = movie.GDetails.Type
            };

            return Ok(movieDto);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> AddMovie(AddMovieDto addMovieDto)
        {
            var director = await _context.DirectorList.FindAsync(addMovieDto.DirectorId);
            if (director == null)
            {
                return NotFound("Director not found.");
            }

            var genre = await _context.GenreList.FindAsync(addMovieDto.GenreId);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }

            var newMovie = new Movie
            {
                Title = addMovieDto.Title,
                ReleaseDate = addMovieDto.ReleaseDate,
                DDetails = director,
                GDetails = genre
            };

            _context.MovieList.Add(newMovie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = newMovie.Id,
                Title = newMovie.Title,
                ReleaseDate = newMovie.ReleaseDate,
                DirectorName = $"{newMovie.DDetails.FirstName} {newMovie.DDetails.LastName}",
                GenreType = newMovie.GDetails.Type
            };

            return Ok(movieDto);
        }

        [HttpPut]
        public async Task<ActionResult<MovieDto>> UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            var dbMovie = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .FirstOrDefaultAsync(m => m.Id == updateMovieDto.Id);

            if (dbMovie == null)
            {
                return NotFound("Movie not found.");
            }

            var director = await _context.DirectorList.FindAsync(updateMovieDto.DirectorId);
            if (director == null)
            {
                return NotFound("Director not found.");
            }

            var genre = await _context.GenreList.FindAsync(updateMovieDto.GenreId);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }

            dbMovie.Title = updateMovieDto.Title;
            dbMovie.ReleaseDate = updateMovieDto.ReleaseDate;
            dbMovie.DDetails = director;
            dbMovie.GDetails = genre;

            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = dbMovie.Id,
                Title = dbMovie.Title,
                ReleaseDate = dbMovie.ReleaseDate,
                DirectorName = $"{dbMovie.DDetails.FirstName} {dbMovie.DDetails.LastName}",
                GenreType = dbMovie.GDetails.Type
            };

            return Ok(movieDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MovieDto>>> DeleteMovie(int id)
        {
            var dbMovie = await _context.MovieList.FindAsync(id);
            if (dbMovie == null)
            {
                return NotFound("Movie not found.");
            }

            _context.MovieList.Remove(dbMovie);
            await _context.SaveChangesAsync();

            var movies = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .ToListAsync();

            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                DirectorName = $"{m.DDetails.FirstName} {m.DDetails.LastName}",
                GenreType = m.GDetails.Type
            }).ToList();

            return Ok(movieDtos);
        }
    }
}