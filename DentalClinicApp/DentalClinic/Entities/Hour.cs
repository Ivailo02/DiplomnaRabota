using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Entities
{
    public class Hour
    {
        [Key]
        public int Id { get; set; }
        public DateTime FreeHour { get; set; }
        public bool IsBusy 
        {
            get
            {
                return ReservationId > 0;
            }
        }
        [Required]
        public int DentistId { get; set; }
        public virtual Dentist Dentist { get; set; }

        [ForeignKey("Reservation")]
        public int? ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }
}
