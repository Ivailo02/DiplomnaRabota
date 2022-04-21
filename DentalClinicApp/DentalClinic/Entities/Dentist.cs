﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Entities
{
    public class Dentist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string EGN { get; set; }

        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set;}

        public virtual ICollection<Hour> Hours { get; set; } = new List<Hour>();
        public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();
    }
}
