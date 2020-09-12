using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class Performance
    {
        [Key]
        public int PerformanceId { get; set; }
        [Required]
        public string PerformanceName { get; set; }

        public string Description { get; set; }

        public int BuildingId { get; set; }


        public Building Building { get; set; }


        public DateTime PerformanceDateTime { get; set; }

        public String ImageURL { get; set; }

        public virtual List<Seat> Seats { get; set; }
        public Performance()
        {
            Seats = new List<Seat>();
        }
    }
}