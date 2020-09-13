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
        
        [Display(Name = "Корисник")]
        public ApplicationUser User { get; set; }

        [Required]
        public int SeatId { get; set; }
        [Required]
        [Display(Name = "Број на седиште")]
        public Seat Seat { get; set; }

      
        public Reservation()
        {

        }
    }
}