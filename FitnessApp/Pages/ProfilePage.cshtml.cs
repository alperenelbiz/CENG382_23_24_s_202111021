using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FitnessApp.Models;
using FitnessApp.Data;

namespace FitnessApp.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfilePageModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ApplicationUser UserProfile { get; set; }
        public string ProfilePictureBase64 { get; set; }
        public IQueryable<Challenge> SavedChallenges { get; set; }

        public async Task OnGetAsync()
        {
            UserProfile = await _userManager.GetUserAsync(User);
            if (UserProfile != null)
            {
                if (UserProfile.ProfilePicture != null)
                {
                    ProfilePictureBase64 = Convert.ToBase64String(UserProfile.ProfilePicture);
                }
                SavedChallenges = _context.Challenges.Where(c => c.Creator.Id == UserProfile.Id);
            }
        }
        public async Task<IActionResult> OnPostSaveChallengeAsync(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.ExistingChallenges == null)
            {
                user.ExistingChallenges = new List<Challenge>();
            }
            user.ExistingChallenges.Add(challenge);
            await _userManager.UpdateAsync(user);

            return RedirectToPage(new { id });
        }

    }
}
