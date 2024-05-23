using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace lab11.Pages
{
    [Authorize]
    public class CreateReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CreateReservationModel> _logger;

        public CreateReservationModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<CreateReservationModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }
        public List<Room> Rooms { get; set; }

        public IActionResult OnGet()
        {
            Rooms = _context.Rooms.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Rooms = _context.Rooms.ToList();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Reservation.ReservedBy = user.UserName;

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Reservation created by {User} for Room {RoomId} at {DateTime}", Reservation.ReservedBy, Reservation.RoomId, Reservation.ReservationDateTime);

            return RedirectToPage("./Index");
        }
    }
}
