using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class UserReservations
    {
        public List<Reservation> UserReservationsList { get; set; }
        public ApplicationUser User { get; set; }

        public UserReservations()
        {
            this.UserReservationsList = new List<Reservation>();
        }
    }
}