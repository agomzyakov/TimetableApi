using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimetableApi.Models;
using System.Linq;

namespace TimetableApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableItemsController : ControllerBase
    {
        // Mocked data
        private static Dictionary<long, string> moviesNames = new Dictionary<long, string>(){
            { 0, "Hot Shots!" },
            { 1, "South Park" },
            { 2, "Mr. Bean's Holiday" },
            { 3, "The Naked Gun" },
            { 4, "TeNeT" },
        };

        // Mocked data
        private static Dictionary<long, string> cinemasNames = new Dictionary<long, string>(){
            { 0, "Yakutsk" },
            { 1, "Almaz" },
            { 2, "Mirnyi" },
            { 3, "Alrosa" },
            { 4, "Cinema Park" },
        };

        private readonly TimetableContext _context;

        public TimetableItemsController(TimetableContext context)
        {
            _context = context;
        }

        // GET: api/TimetableItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimetableItemDTO>>> GetTimetableItems()
        {
            if (_context.TimetableItems == null)
            {
                return NotFound();
            }

            return (await _context.TimetableItems.ToListAsync())
                .Select(item => new TimetableItemDTO
                {
                    Id = item.Id,
                    DateTime = item.DateTime,
                    Movie = moviesNames[item.MovieId % 5],
                    Cinema = cinemasNames[item.CinemaId % 5]
                })
                .ToList();
        }

        // GET: api/TimetableItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimetableItemDTO>> GetTimetableItem(long id)
        {
            if (_context.TimetableItems == null)
            {
                return NotFound();
            }

            var timetableItem = await _context.TimetableItems.FindAsync(id);

            if (timetableItem == null)
            {
                return NotFound();
            }

            return new TimetableItemDTO
            {
                Id = timetableItem.Id,
                DateTime = timetableItem.DateTime,
                Movie = moviesNames[timetableItem.MovieId % 5],
                Cinema = cinemasNames[timetableItem.CinemaId % 5]
            };
        }

        // PUT: api/TimetableItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimetableItem(long id, TimetableItem timetableItem)
        {
            if (id != timetableItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(timetableItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableItemExists(id))
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

        // POST: api/TimetableItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimetableItem>> PostTimetableItem(TimetableItem timetableItem)
        {
          if (_context.TimetableItems == null)
          {
              return Problem("Entity set 'TimetableContext.TimetableItems'  is null.");
          }
            _context.TimetableItems.Add(timetableItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimetableItem), new { id = timetableItem.Id }, timetableItem);
        }

        // DELETE: api/TimetableItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetableItem(long id)
        {
            if (_context.TimetableItems == null)
            {
                return NotFound();
            }
            var timetableItem = await _context.TimetableItems.FindAsync(id);
            if (timetableItem == null)
            {
                return NotFound();
            }

            _context.TimetableItems.Remove(timetableItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimetableItemExists(long id)
        {
            return (_context.TimetableItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
