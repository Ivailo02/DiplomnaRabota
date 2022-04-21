using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Data;
using DentalClinic.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DentalClinic.Models.Patient;
using DentalClinic.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace DentalClinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;

        public int GlobalMessageKey { get; private set; }

        public PatientsController(IPatientService patientService, UserManager<ApplicationUser> userManager)
        {
            _patientService = patientService;
            _userManager = userManager;
        }


        // GET: PatientController
        public ActionResult Index()
        {
            var dentists = _patientService.GetPatients().Select(u => new ListingPatientVM
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EGN = u.EGN,
                Phone = u.Phone,
                BirthDay = u.BirthDay
            }).ToList();
            return View(dentists);
        }

        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: PatientController/Create
        public ActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userIdAlreadyPatient = this._patientService
                .GetPatients()
                .Any(d => d.UserId == userId);

            if (userIdAlreadyPatient)
            {

                return RedirectToAction("Index", "Hours");
            }
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePatientVM patient)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userIdAlreadyPatient = this._patientService
                .GetPatients()
                .Any(d => d.UserId == userId);



            if (!ModelState.IsValid)
            {
                return View(patient);
            }



            var created = _patientService.CreatePacient(patient.FirstName, patient.LastName, patient.EGN, patient.Phone, patient.BirthDay, userId);

            return RedirectToAction("Index", "Hours");

            //   var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);



        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
