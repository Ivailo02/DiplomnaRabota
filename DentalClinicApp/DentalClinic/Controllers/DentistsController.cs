using DentalClinic.Entities;
using DentalClinic.Models.Dentist;
using DentalClinic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalClinic.Controllers
{
    public class DentistsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDentistService _dentistService;

        public DentistsController(UserManager<ApplicationUser> userManager, IDentistService dentistService)
        {
            _userManager = userManager;
            _dentistService = dentistService;
        }

        // GET: DentistsControler
        public ActionResult Index()
        {
            var dentists = _dentistService.GetDentists().Select(u => new ListingDentisVM
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EGN = u.EGN,
                Phone = u.Phone,
                Specialty = u.Specialty
            }).ToList();
            return View(dentists);
        }

        // GET: DentistsControler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DentistsControler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DentistsControler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDentistVM dentist)
        {
            if (!ModelState.IsValid)
            {
                return View(dentist);
            }
            if (await _userManager.FindByNameAsync(dentist.Username) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = dentist.Username;
                user.Email = dentist.Email; 
            
            var result = await _userManager.CreateAsync(user, "123456!");
                if (result.Succeeded)
                {
                    var created = _dentistService.CreateDentist(dentist.FirstName, dentist.LastName, dentist.Phone, dentist.EGN, dentist.Specialty, user.Id);
                    if (created)
                    {
                        _userManager.AddToRoleAsync(user, "Dentist").Wait();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "The employee exist");
            return View();
        }


        // GET: DentistsControler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DentistsControler/Edit/5
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

        // GET: DentistsControler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DentistsControler/Delete/5
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
