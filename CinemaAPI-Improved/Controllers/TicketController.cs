using CinemaAPI_Improved.Data;
using CinemaAPI_Improved.Dtos;
using CinemaAPI_Improved.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI_Improved.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DataContext _context;
        public TicketController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
        {
            var tickets = await _context.TicketList
                .Include(t => t.Mdetails)
                    .ThenInclude(m => m.DDetails)
                .Include(t => t.Mdetails)
                    .ThenInclude(m => m.GDetails)
                .Include(t => t.Vdetails)
                .ToListAsync();

            var ticketDtos = tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                PurchaseDate = t.PurchaseDate,
                Mdetails = new MovieDto
                {
                    Id = t.Mdetails.Id,
                    Title = t.Mdetails.Title,
                    ReleaseDate = t.Mdetails.ReleaseDate,
                    DirectorName = $"{t.Mdetails.DDetails.FirstName} {t.Mdetails.DDetails.LastName}",
                    GenreType = t.Mdetails.GDetails.Type
                },
                Vdetails = new ViewerDto
                {
                    Id = t.Vdetails.Id,
                    FirstName = t.Vdetails.FirstName,
                    LastName = t.Vdetails.LastName,
                    ContactNumber = t.Vdetails.ContactNumber,
                    Email = t.Vdetails.Email
                }
            }).ToList();

            return Ok(ticketDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var specificTicket = await _context.TicketList
                .Include(t => t.Mdetails)
                    .ThenInclude(m => m.DDetails)
                .Include(t => t.Mdetails)
                    .ThenInclude(m => m.GDetails)
                .Include(t => t.Vdetails)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (specificTicket is null)
            {
                return NotFound("Ticket not found.");
            }

            var ticketDto = new TicketDto
            {
                Id = specificTicket.Id,
                PurchaseDate = specificTicket.PurchaseDate,
                Mdetails = new MovieDto
                {
                    Id = specificTicket.Mdetails.Id,
                    Title = specificTicket.Mdetails.Title,
                    ReleaseDate = specificTicket.Mdetails.ReleaseDate,
                    DirectorName = $"{specificTicket.Mdetails.DDetails.FirstName} {specificTicket.Mdetails.DDetails.LastName}",
                    GenreType = specificTicket.Mdetails.GDetails.Type
                },
                Vdetails = new ViewerDto
                {
                    Id = specificTicket.Vdetails.Id,
                    FirstName = specificTicket.Vdetails.FirstName,
                    LastName = specificTicket.Vdetails.LastName,
                    ContactNumber = specificTicket.Vdetails.ContactNumber,
                    Email = specificTicket.Vdetails.Email
                }
            };

            return Ok(ticketDto);
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> AddTicket(AddTicketDto addTicketDto)
        {
            var movie = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .FirstOrDefaultAsync(m => m.Id == addTicketDto.MovieId);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var viewer = await _context.ViewersList.FindAsync(addTicketDto.ViewerId);
            if (viewer == null)
            {
                return NotFound("Viewer not found.");
            }

            var newTicket = new Ticket
            {
                PurchaseDate = addTicketDto.PurchaseDate,
                Mdetails = movie,
                Vdetails = viewer
            };

            _context.TicketList.Add(newTicket);
            await _context.SaveChangesAsync();

            var ticketDto = new TicketDto
            {
                Id = newTicket.Id,
                PurchaseDate = newTicket.PurchaseDate,
                Mdetails = new MovieDto
                {
                    Id = newTicket.Mdetails.Id,
                    Title = newTicket.Mdetails.Title,
                    ReleaseDate = newTicket.Mdetails.ReleaseDate,
                    DirectorName = $"{newTicket.Mdetails.DDetails.FirstName} {newTicket.Mdetails.DDetails.LastName}",
                    GenreType = newTicket.Mdetails.GDetails.Type
                },
                Vdetails = new ViewerDto
                {
                    Id = newTicket.Vdetails.Id,
                    FirstName = newTicket.Vdetails.FirstName,
                    LastName = newTicket.Vdetails.LastName,
                    ContactNumber = newTicket.Vdetails.ContactNumber,
                    Email = newTicket.Vdetails.Email
                }
            };

            return Ok(ticketDto);
        }

        [HttpPut]
        public async Task<ActionResult<TicketDto>> UpdateTicket(UpdateTicketDto updateTicketDto)
        {
            var dbTicket = await _context.TicketList
                .Include(t => t.Mdetails)
                .Include(t => t.Vdetails)
                .FirstOrDefaultAsync(t => t.Id == updateTicketDto.Id);

            if (dbTicket is null)
            {
                return NotFound("Ticket not found");
            }

            var movie = await _context.MovieList
                .Include(m => m.DDetails)
                .Include(m => m.GDetails)
                .FirstOrDefaultAsync(m => m.Id == updateTicketDto.MovieId);

            if (movie == null)
            {
                return NotFound($"Movie with ID {updateTicketDto.MovieId} not found.");
            }

            var viewer = await _context.ViewersList.FindAsync(updateTicketDto.ViewerId);
            if (viewer == null)
            {
                return NotFound($"Viewer with ID {updateTicketDto.ViewerId} not found.");
            }

            dbTicket.PurchaseDate = updateTicketDto.PurchaseDate;
            dbTicket.Mdetails = movie;
            dbTicket.Vdetails = viewer;

            await _context.SaveChangesAsync();

            var ticketDto = new TicketDto
            {
                Id = dbTicket.Id,
                PurchaseDate = dbTicket.PurchaseDate,
                Mdetails = new MovieDto
                {
                    Id = dbTicket.Mdetails.Id,
                    Title = dbTicket.Mdetails.Title,
                    ReleaseDate = dbTicket.Mdetails.ReleaseDate,
                    DirectorName = $"{dbTicket.Mdetails.DDetails.FirstName} {dbTicket.Mdetails.DDetails.LastName}",
                    GenreType = dbTicket.Mdetails.GDetails.Type
                },
                Vdetails = new ViewerDto
                {
                    Id = dbTicket.Vdetails.Id,
                    FirstName = dbTicket.Vdetails.FirstName,
                    LastName = dbTicket.Vdetails.LastName,
                    ContactNumber = dbTicket.Vdetails.ContactNumber,
                    Email = dbTicket.Vdetails.Email
                }
            };

            return Ok(ticketDto);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Ticket>>> DeleteTicket(int id)
        {
            var dbTicket = await _context.TicketList.FindAsync(id);
            if (dbTicket is null)
            {
                return NotFound("Ticket not found");
            }

            _context.TicketList.Remove(dbTicket);
            await _context.SaveChangesAsync();

            return Ok(await _context.TicketList.ToListAsync());
        }
    }
}
