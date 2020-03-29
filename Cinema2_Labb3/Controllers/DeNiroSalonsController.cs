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
    public class DeNiroSalonsController : Controller
    {
        private readonly BerrasBiografContext _context;

        public DeNiroSalonsController(BerrasBiografContext context)
        {
            _context = context;
        }

        // GET: DeNiroSalons
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeNiroSalon.ToListAsync());
        }

        // GET: DeNiroSalons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deNiroSalon = await _context.DeNiroSalon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deNiroSalon == null)
            {
                return NotFound();
            }

            return View(deNiroSalon);
        }

        // GET: DeNiroSalons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeNiroSalons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Availible")] DeNiroSalon deNiroSalon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deNiroSalon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deNiroSalon);
        }

        // GET: DeNiroSalons/Edit/5
        public async Task<IActionResult> EditAvailible(string fullname, string email, string entities, string NoOfTickets, string movie, string price, string time, string salon, string actualSeats)
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

            return RedirectToAction("Payment", "Orders", new { fullname = fullname, email = email, entities = entities, NoOfTickets = NoOfTickets, movie = movie, price = price, time = time, salon = salon, actualSeats = actualSeats });

        }

        public IActionResult ConfirmOrder(int NoOftickets, int[] seats, string movie, int price, string time)
        {
            string confirmedSeats = "";

            foreach (var seat in seats)
            {
                seat.ToString();

                confirmedSeats = confirmedSeats + seat;
            }


            ViewBag.tickets = NoOftickets;
            ViewBag.seats = confirmedSeats;
            ViewBag.movie = movie;
            ViewBag.price = price;
            ViewBag.time = time;


            return View();
        }

        // POST: DeNiroSalons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Availible")] DeNiroSalon deNiroSalon)
        {
            if (id != deNiroSalon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deNiroSalon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeNiroSalonExists(deNiroSalon.Id))
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
            return View(deNiroSalon);
        }

        // GET: DeNiroSalons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deNiroSalon = await _context.DeNiroSalon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deNiroSalon == null)
            {
                return NotFound();
            }

            return View(deNiroSalon);
        }

        // POST: DeNiroSalons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deNiroSalon = await _context.DeNiroSalon.FindAsync(id);
            _context.DeNiroSalon.Remove(deNiroSalon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeNiroSalonExists(int id)
        {
            return _context.DeNiroSalon.Any(e => e.Id == id);
        }
    }
}
