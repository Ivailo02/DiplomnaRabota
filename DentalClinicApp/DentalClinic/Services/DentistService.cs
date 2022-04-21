using DentalClinic.Data;
using DentalClinic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Services
{
    public class DentistService : IDentistService
    {
        private readonly ApplicationDbContext _context;

        public DentistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateDentist(string firstName, string lastName, string egn, string phone, string specialty, string userId)
        {
            if (_context.Dentists.Any(p => p.UserId == userId))
            {
                throw new InvalidOperationException("Employee already exist.");
            }
            Dentist dentistForDb = new Dentist()
            {
                FirstName = firstName,
                LastName = lastName,
                EGN = egn,
                Phone = phone,
                Specialty = specialty,
                UserId = userId
            };

            _context.Dentists.Add(dentistForDb);

            return _context.SaveChanges() != 0;
        }

        public Dentist GetDentistById(int dentistId)
        {
            throw new NotImplementedException();
        }

        public List<Dentist> GetDentists()
        {
            return _context.Dentists.ToList();
        }

        public Dentist GetDentistsById(int dentistid)
        {
            return _context.Dentists.Find(dentistid);
        }
    }
}
