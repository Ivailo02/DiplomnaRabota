using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Data;
using DentalClinic.Entities;
using System.Security.Claims;

namespace DentalClinic.Controllers
{
    public class HoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hours
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hours.Include(h => h.Dentist).Include(h => h.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> MyDentistHour()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.Hours.Where(x => x.Dentist.UserId == currentUserId).Include(r => r.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hour = await _context.Hours
                .Include(h => h.Dentist)
                .Include(h => h.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hour == null)
            {
                return NotFound();
            }

            return View(hour);
        }

        // GET: Hours/Create
        public IActionResult Create()
        {
            ViewData["DentistId"] = new SelectList(_context.Dentists, "Id", "Phone");
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id");
            return View();
        }

        // POST: Hours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FreeHour,DentistId,ReservationId")] Hour hour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentistId"] = new SelectList(_context.Dentists, "Id", "Phone", hour.DentistId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", hour.ReservationId);
            return View(hour);
        }

        // GET: Hours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hour = await _context.Hours.FindAsync(id);
            if (hour == null)
            {
                return NotFound();
            }
            ViewData["DentistId"] = new SelectList(_context.Dentists, "Id", "EGN", hour.DentistId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", hour.ReservationId);
            return View(hour);
        }

        // POST: Hours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FreeHour,DentistId,ReservationId")] Hour hour)
        {
            if (id != hour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HourExists(hour.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentistId"] = new SelectList(_context.Dentists, "Id", "Phone", hour.DentistId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", hour.ReservationId);
            return View(hour);
        }

        // GET: Hours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hour = await _context.Hours
                .Include(h => h.Dentist)
                .Include(h => h.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hour == null)
            {
                return NotFound();
            }

            return View(hour);
        }

        // POST: Hours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hour = await _context.Hours.FindAsync(id);
            _context.Hours.Remove(hour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HourExists(int id)
        {
            return _context.Hours.Any(e => e.Id == id);
        }
    }
}
