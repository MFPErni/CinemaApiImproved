using CinemaAPI_Improved.Data;
using CinemaAPI_Improved.Dtos;
using CinemaAPI_Improved.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI_Improved.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DataContext _context;

        public DirectorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DirectorDto>>> GetAllDirectors()
        {
            var directors = await _context.DirectorList.ToListAsync();
            var directorDtos = directors.Select(d => new DirectorDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthdate = d.Birthdate
            }).ToList();

            return Ok(directorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDto>> GetDirector(int id)
        {
            var director = await _context.DirectorList.FindAsync(id);
            if (director == null)
            {
                return NotFound("Director not found.");
            }

            var directorDto = new DirectorDto
            {
                Id = director.Id,
                FirstName = director.FirstName,
                LastName = director.LastName,
                Birthdate = director.Birthdate
            };

            return Ok(directorDto);
        }

        [HttpPost]
        public async Task<ActionResult<DirectorDto>> AddDirector(AddDirectorDto addDirectorDto)
        {
            var newDirector = new Director
            {
                FirstName = addDirectorDto.FirstName,
                LastName = addDirectorDto.LastName,
                Birthdate = addDirectorDto.Birthdate
            };

            _context.DirectorList.Add(newDirector);
            await _context.SaveChangesAsync();

            var directorDto = new DirectorDto
            {
                Id = newDirector.Id,
                FirstName = newDirector.FirstName,
                LastName = newDirector.LastName,
                Birthdate = newDirector.Birthdate
            };

            return Ok(directorDto);
        }

        [HttpPut]
        public async Task<ActionResult<DirectorDto>> UpdateDirector(UpdateDirectorDto updateDirectorDto)
        {
            var dbDirector = await _context.DirectorList.FindAsync(updateDirectorDto.Id);
            if (dbDirector == null)
            {
                return NotFound("Director not found.");
            }

            dbDirector.FirstName = updateDirectorDto.FirstName;
            dbDirector.LastName = updateDirectorDto.LastName;
            dbDirector.Birthdate = updateDirectorDto.Birthdate;

            await _context.SaveChangesAsync();

            var directorDto = new DirectorDto
            {
                Id = dbDirector.Id,
                FirstName = dbDirector.FirstName,
                LastName = dbDirector.LastName,
                Birthdate = dbDirector.Birthdate
            };

            return Ok(directorDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<DirectorDto>>> DeleteDirector(int id)
        {
            var dbDirector = await _context.DirectorList.FindAsync(id);
            if (dbDirector == null)
            {
                return NotFound("Director not found.");
            }

            _context.DirectorList.Remove(dbDirector);
            await _context.SaveChangesAsync();

            var directors = await _context.DirectorList.ToListAsync();
            var directorDtos = directors.Select(d => new DirectorDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthdate = d.Birthdate
            }).ToList();

            return Ok(directorDtos);
        }
    }
}