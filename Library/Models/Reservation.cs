using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
