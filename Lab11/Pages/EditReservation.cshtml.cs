using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab11.Pages
{
    [Authorize]
    public class EditReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }
        public List<Room> Rooms { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _context.Reservations.FindAsync(id);

            if (Reservation == null)
            {
                return NotFound();
            }

            Rooms = await _context.Rooms.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Rooms = await _context.Rooms.ToListAsync();
                return Page();
            }

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reservations.Any(e => e.Id == Reservation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Reservations");
        }
    }
}
