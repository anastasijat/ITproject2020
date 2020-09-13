using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        //public int ClientId { get; set; }

        //public Client Client { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int SeatId { get; set; }
        [Required]
        public Seat Seat { get; set; }

      
        public Reservation()
        {

        }
    }
}