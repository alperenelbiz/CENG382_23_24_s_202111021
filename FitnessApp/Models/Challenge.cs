using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models
{

    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Period { get; set; }
        public string Difficulty { get; set; }
        public string Instructions { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime CreatTime { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

}
