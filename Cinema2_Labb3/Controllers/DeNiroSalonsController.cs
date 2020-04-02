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

        // GET: DeNiroSalons/Edit/5
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

    }
}
