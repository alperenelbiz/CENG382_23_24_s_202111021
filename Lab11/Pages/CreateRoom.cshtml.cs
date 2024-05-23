using System;
using System.Threading.Tasks;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;


namespace lab11.Pages
{
    [Authorize]
    public class CreateRoomModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateRoomModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
