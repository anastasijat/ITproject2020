using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class SeatListModel
    {
        public IList<int> selectedSeats { get; set; }
        public IList<Seat> availableSeats { get; set; }
        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }

        public SeatListModel()
        {
            selectedSeats = new List<int>();
            availableSeats = new List<Seat>();

        }
    }
}