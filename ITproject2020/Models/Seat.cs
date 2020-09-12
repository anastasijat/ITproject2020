using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        [Required]
        public int SeatNumber { get; set; }

        public Boolean status { get; set; }
        public int Price { get; set; }

        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }

        public Seat()
        {

        }
        public Seat(int SeatNumber, int PerformanceId, Performance Performance)
        {
            this.SeatNumber = SeatNumber;
            this.status = false;
            this.PerformanceId = PerformanceId;
            this.Performance = Performance;
        }
    }
}