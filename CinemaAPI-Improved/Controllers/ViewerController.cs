using CinemaAPI_Improved.Data;
using CinemaAPI_Improved.Dtos;
using CinemaAPI_Improved.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI_Improved.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController : ControllerBase
    {
        private readonly DataContext _context;

        public ViewerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewerDto>>> GetAllViewers()
        {
            var viewers = await _context.ViewersList.ToListAsync();
            var viewerDtos = viewers.Select(v => new ViewerDto
            {
                Id = v.Id,
                FirstName = v.FirstName,
                LastName = v.LastName,
                ContactNumber = v.ContactNumber,
                Email = v.Email
            }).ToList();

            return Ok(viewerDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewerDto>> GetViewer(int id)
        {
            var viewer = await _context.ViewersList.FindAsync(id);
            if (viewer == null)
            {
                return NotFound("Viewer not found.");
            }

            var viewerDto = new ViewerDto
            {
                Id = viewer.Id,
                FirstName = viewer.FirstName,
                LastName = viewer.LastName,
                ContactNumber = viewer.ContactNumber,
                Email = viewer.Email
            };

            return Ok(viewerDto);
        }

        [HttpPost]
        public async Task<ActionResult<ViewerDto>> AddViewer(AddViewerDto addViewerDto)
        {
            var newViewer = new Viewer
            {
                FirstName = addViewerDto.FirstName,
                LastName = addViewerDto.LastName,
                ContactNumber = addViewerDto.ContactNumber,
                Email = addViewerDto.Email
            };

            _context.ViewersList.Add(newViewer);
            await _context.SaveChangesAsync();

            var viewerDto = new ViewerDto
            {
                Id = newViewer.Id,
                FirstName = newViewer.FirstName,
                LastName = newViewer.LastName,
                ContactNumber = newViewer.ContactNumber,
                Email = newViewer.Email
            };

            return Ok(viewerDto);
        }

        [HttpPut]
        public async Task<ActionResult<ViewerDto>> UpdateViewer(UpdateViewerDto updateViewerDto)
        {
            var dbViewer = await _context.ViewersList.FindAsync(updateViewerDto.Id);
            if (dbViewer == null)
            {
                return NotFound("Viewer not found.");
            }

            dbViewer.FirstName = updateViewerDto.FirstName;
            dbViewer.LastName = updateViewerDto.LastName;
            dbViewer.ContactNumber = updateViewerDto.ContactNumber;
            dbViewer.Email = updateViewerDto.Email;

            await _context.SaveChangesAsync();

            var viewerDto = new ViewerDto
            {
                Id = dbViewer.Id,
                FirstName = dbViewer.FirstName,
                LastName = dbViewer.LastName,
                ContactNumber = dbViewer.ContactNumber,
                Email = dbViewer.Email
            };

            return Ok(viewerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ViewerDto>>> DeleteViewer(int id)
        {
            var dbViewer = await _context.ViewersList.FindAsync(id);
            if (dbViewer == null)
            {
                return NotFound("Viewer not found.");
            }

            _context.ViewersList.Remove(dbViewer);
            await _context.SaveChangesAsync();

            var viewers = await _context.ViewersList.ToListAsync();
            var viewerDtos = viewers.Select(v => new ViewerDto
            {
                Id = v.Id,
                FirstName = v.FirstName,
                LastName = v.LastName,
                ContactNumber = v.ContactNumber,
                Email = v.Email
            }).ToList();

            return Ok(viewerDtos);
        }
    }
}