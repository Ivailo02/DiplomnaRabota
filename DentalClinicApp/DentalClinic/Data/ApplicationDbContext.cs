using DentalClinic.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DentalClinic.Models.Dentist;
using DentalClinic.Models.Patient;

namespace DentalClinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<DentalClinic.Models.Dentist.CreateDentistVM> CreateDentistVM { get; set; }

        public DbSet<DentalClinic.Models.Dentist.ListingDentisVM> ListingDentisVM { get; set; }

        public DbSet<DentalClinic.Models.Patient.CreatePatientVM> CreatePatientVM { get; set; }

        public DbSet<DentalClinic.Models.Patient.ListingPatientVM> ListingPatientVM { get; set; }
    }
}
