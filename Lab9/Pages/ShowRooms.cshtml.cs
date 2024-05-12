using System.Collections.Generic;
using Lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab9.Pages
{
    public class ShowRoomsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ShowRoomsModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Room> Rooms { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = await _context.Rooms.ToListAsync();
            return Page();
        }
    }
}
