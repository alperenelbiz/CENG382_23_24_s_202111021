using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab11.Models;
using lab11.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace lab11.Pages
{
    [Authorize]
    public class ReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public ReservationFilter Filter { get; set; }

        public List<Room> Rooms { get; set; }
        public Dictionary<(DateTime Date, TimeSpan Time), Reservation> Reservations { get; set; }
        public List<DateTime> WeekDays { get; set; }
        public List<TimeSpan> Hours { get; set; }

        public void OnGet()
        {
            Rooms = _context.Rooms.ToList();

            if (Filter == null)
            {
                Filter = new ReservationFilter
                {
                    StartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday),
                    EndDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(6)
                };
            }

            WeekDays = Enumerable.Range(0, 7).Select(i => Filter.StartDate.AddDays(i)).ToList();
            Hours = Enumerable.Range(0, 24).Select(i => TimeSpan.FromHours(i)).ToList();

            var query = _context.Reservations.Include(r => r.Room).AsQueryable();

            if (!string.IsNullOrEmpty(Filter.RoomName))
            {
                query = query.Where(r => r.Room.RoomName.Contains(Filter.RoomName));
            }

            if (Filter.StartDate != DateTime.MinValue && Filter.EndDate != DateTime.MinValue)
            {
                query = query.Where(r => r.ReservationDateTime.Date >= Filter.StartDate.Date && r.ReservationDateTime.Date <= Filter.EndDate.Date);
            }

            if (Filter.Capacity > 0)
            {
                query = query.Where(r => r.Room.Capacity >= Filter.Capacity);
            }

            Reservations = query.ToList().ToDictionary(r => (r.ReservationDateTime.Date, r.ReservationDateTime.TimeOfDay));
        }
    }

    public class ReservationFilter
    {
        public string RoomName { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int Capacity { get; set; } = 0;
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
