using System;
using System.Collections.Generic;
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
    public class ReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList Rooms { get; set; }

        public List<Reservation> Reservations { get; set; }

        [BindProperty]
        public int RoomId { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
            StartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday); // Assumes a method to get the start of the week
            Reservations = new List<Reservation>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");

            var endDate = StartDate.AddDays(7);
            if (RoomId == 0)
            {
                // Show all reservations for the week
                Reservations = await _context.Reservations
                    .Include(r => r.Room)
                    .Where(r => r.ReservationDateTime >= StartDate && r.ReservationDateTime < endDate)
                    .ToListAsync();
            }
            else
            {
                // Filter reservations by selected room
                Reservations = await _context.Reservations
                    .Include(r => r.Room)
                    .Where(r => r.RoomId == RoomId && r.ReservationDateTime >= StartDate && r.ReservationDateTime < endDate)
                    .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                await LogActionAsync("Delete", "Reservation");
            }

            return RedirectToPage(new { RoomId, StartDate });
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

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
