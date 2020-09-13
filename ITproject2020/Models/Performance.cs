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
        [Display(Name = "Изведба")]
        public string PerformanceName { get; set; }

        [Display(Name ="Опис")]
        public string Description { get; set; }

        [Display(Name = "Место на одржување")]
        public int BuildingId { get; set; }

        [Display(Name = "Место на одржување")]
        public Building Building { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Датум и време на одржување")]
        public DateTime PerformanceDateTime { get; set; }
        [Display(Name = "Слика")]
        public String ImageURL { get; set; }

        public virtual List<Seat> Seats { get; set; }
        public Performance()
        {
            Seats = new List<Seat>();
        }
    }
}