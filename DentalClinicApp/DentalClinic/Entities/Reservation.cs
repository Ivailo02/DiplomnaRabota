using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [MinLength(10)]
        [MaxLength(50)]
        public string Reason { get; set; }

        [Required]
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        [Required]
        public int HourId { get; set; }
        public virtual Hour Hour {get; set;}
    }
}
