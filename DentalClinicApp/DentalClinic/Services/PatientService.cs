using DentalClinic.Abstractions;
using DentalClinic.Data;
using DentalClinic.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Services
{
    public class PatientService : IPatientService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public bool CreatePacient(string firstName, string lastName, string egn, string phone, DateTime birthDay, string userId)
        {
            if (_context.Patients.Any(p => p.UserId == userId))
            {
                throw new InvalidOperationException("Patient already exist.");
            }
            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                EGN = egn,
                Phone = phone,
                BirthDay = birthDay,
                UserId = userId,
            };

            _context.Patients.Add(patient);
            return _context.SaveChanges() != 0;
        }
        public List<Patient> GetPatients()
        {
        List<Patient> patients = _context.Patients
            .ToList();
        return patients;
        }
    }
   
}

