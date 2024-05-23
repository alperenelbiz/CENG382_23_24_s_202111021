using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Authorization;

namespace lab11.Pages
{
    [Authorize]
    public class DeleteReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _context.Reservations.FindAsync(id);

            if (Reservation == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Reservations");
        }
    }
}
