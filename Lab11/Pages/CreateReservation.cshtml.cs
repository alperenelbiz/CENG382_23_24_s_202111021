using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab11.Data;
using lab11.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

            var oldReservation = await _context.Reservations.FirstOrDefaultAsync(r => r.RoomId == Reservation.RoomId && r.ReservationDateTime == Reservation.ReservationDateTime);

            if (oldReservation != null)
            {
                ModelState.AddModelError("", "There is another reservation in that time.");
                Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
                return Page();
            }
            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Reservations");
        }
    }
}