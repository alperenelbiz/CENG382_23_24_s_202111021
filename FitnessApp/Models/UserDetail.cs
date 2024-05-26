using Microsoft.AspNetCore.Identity;
using FitnessApp.Models;
using FitnessApp.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FitnessApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public ICollection<Challenge> ExistingChallenges { get; set; } = new List<Challenge>();
    }
}
