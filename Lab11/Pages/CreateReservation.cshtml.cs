using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;

namespace MyApp.Namespace
{
    public class CreateReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public SelectList Rooms { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Reservation.ReservationDateTime < DateTime.UtcNow)
            {
                ModelState.AddModelError("", "Please enter a valid date in the future!");
                Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
                return Page();
            }

            var oldReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.RoomId == Reservation.RoomId && r.ReservationDateTime == Reservation.ReservationDateTime);

            if (oldReservation != null)
            {
                ModelState.AddModelError("", "There is another reservation at that time.");
                Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            await LogActionAsync("Create", "Reservation");

            return RedirectToPage("./Reservations");
        }

        private async Task LogActionAsync(string action, string entity)
        {
            var logEntry = new LogEntry
            {
                Action = action,
                Entity = entity,
                Timestamp = DateTime.UtcNow
            };

            _context.LogEntries.Add(logEntry);
            await _context.SaveChangesAsync();
        }
    }
}
