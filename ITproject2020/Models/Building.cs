using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITproject2020.Models
{
    public class Building
    {
        [Key]
        [Display(Name = "Место на одржување")]
        public int BuildingId { get; set; }

        [Required]
        [Display(Name ="Место на одржување")]
        public string BuildingName { get; set; }

        [Display(Name = "Тип")]
        public string Status { get; set; }//1- teatar, 2-balet


        [Required]
        [Display(Name = "Број на седишта")]
        public int NumberOfSeats { get; set; }
        [Display(Name = "Адреса")]
        public String BuildingAddress { get; set; }

        public virtual List<Performance> Performances { get; set; }

        public Building()
        {
            Performances = new List<Performance>();
        }
    }
}