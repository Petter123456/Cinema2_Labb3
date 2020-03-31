using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema_Labb3.Models;

namespace Cinema2_Labb3.Controllers
{
    public class PaccinoSalonsController : Controller
    {
        private readonly BerrasBiografContext _context;

        public PaccinoSalonsController(BerrasBiografContext context)
        {
            _context = context;
        }

        // GET: PaccinoSalons
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaccinoSalon.ToListAsync());
        }

        // GET: PaccinoSalons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paccinoSalon = await _context.PaccinoSalon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paccinoSalon == null)
            {
                return NotFound();
            }

            return View(paccinoSalon);
        }

        // GET: PaccinoSalons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaccinoSalons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Availible")] PaccinoSalon paccinoSalon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paccinoSalon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paccinoSalon);
        }

        // GET: PaccinoSalons/Edit/5
        public async Task<IActionResult> EditAvailible(string creditCard, string expiryDate, string cvc, string movie, string fullname, string email, string entities, string NoOfTickets, string price, string time, string salon, string actualSeats)
        {


            List<string> numbers = new List<string>(entities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            foreach (var entity in numbers)
            {
                var parseToInt = Int32.Parse(entity);
                var DeNiroSalon = await _context.DeNiroSalon.FindAsync(parseToInt);
                DeNiroSalon.Availible = false;
                _context.Update(DeNiroSalon);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Checkout", "Orders", new { creditCard = creditCard, expiryDate = expiryDate, cvc = cvc, movie = movie, fullname = fullname, email = email, numberOfTickets = NoOfTickets, NoOfTickets = NoOfTickets, totalPrice = price, time = time, salon = salon, actualSeats = actualSeats });

        }



        // POST: PaccinoSalons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Availible")] PaccinoSalon paccinoSalon)
        {
            if (id != paccinoSalon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paccinoSalon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaccinoSalonExists(paccinoSalon.Id))
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
            return View(paccinoSalon);
        }

        // GET: PaccinoSalons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paccinoSalon = await _context.PaccinoSalon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paccinoSalon == null)
            {
                return NotFound();
            }

            return View(paccinoSalon);
        }

        // POST: PaccinoSalons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paccinoSalon = await _context.PaccinoSalon.FindAsync(id);
            _context.PaccinoSalon.Remove(paccinoSalon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaccinoSalonExists(int id)
        {
            return _context.PaccinoSalon.Any(e => e.Id == id);
        }


    }
}
