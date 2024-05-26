using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using FitnessApp.Models;
using FitnessApp.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp
{
    public class CreateChallengeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateChallengeModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Challenge Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                Input.Creator = user;
                Input.CreatTime = DateTime.Now;
                _context.Challenges.Add(Input);
                await _context.SaveChangesAsync();
                return RedirectToPage("ChallengesList");
            }
            return Page();
        }
    }


}
