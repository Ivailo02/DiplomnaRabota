using DentalClinic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Abstractions
{
    public interface IPatientService
    {
        public List<Patient> GetPatients();
        public bool CreatePacient(string firstName, string lastName, string egn, string phone, DateTime birthDay, string userId);
    }
}
