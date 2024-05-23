using System;
using System.ComponentModel.DataAnnotations;

namespace lab11.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        public string ReservedBy { get; set; }
    }
}
