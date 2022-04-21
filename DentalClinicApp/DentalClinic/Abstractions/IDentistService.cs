using DentalClinic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Services
{
    public interface IDentistService
    {
        List<Dentist> GetDentists();
        Dentist GetDentistById(int dentistId);
        bool CreateDentist(string firstName, string lastName, string egn, string phone, string specialty, string userId);
    }
}
