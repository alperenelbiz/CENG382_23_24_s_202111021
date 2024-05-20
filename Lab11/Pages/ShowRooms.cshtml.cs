using System.Collections.Generic;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace lab11.Pages
{
    [Authorize]
    public class ShowRoomsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ShowRoomsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Room = await _context.Room.ToListAsync();
            return Page();
        }
    }
}
