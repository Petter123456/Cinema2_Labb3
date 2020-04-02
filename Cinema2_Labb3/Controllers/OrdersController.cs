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
    public class OrdersController : Controller
    {
        private readonly BerrasBiografContext _context;
        private Orders newOrder;

        public OrdersController(BerrasBiografContext context)
        {
            _context = context;
        }

        private bool OrdersExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public IActionResult OrderConfirmation(string movie, string fullname, string email, int price, int numberOfTicket)
        {
            var totalPrice = numberOfTicket * price;

            return RedirectToAction("Payment", "Orders", new { movie = movie, fullname = fullname, email = email, price = price, numberOfTicket = numberOfTicket, totalPrice = totalPrice });
        }

        public async Task<IActionResult> SelectSeatsDeNiroSalon(string name, int price, string time, string salon)
        {

            ViewBag.movie = name.Trim().ToString();
            ViewBag.price = price.ToString().Trim();
            ViewBag.time = time.Trim().ToString();
            ViewBag.salon = salon.Trim().ToString();

            return View(await _context.DeNiroSalon.ToListAsync());
        }

        public async Task<IActionResult> SelectSeatsPaccinoSalon(string name, int price, string time, string salon)
        {
   
            ViewBag.movie = name.Trim().ToString();
            ViewBag.price = price.ToString().Trim();
            ViewBag.time = time.Trim().ToString();
            ViewBag.salon = salon.Trim().ToString();

            return View(await _context.PaccinoSalon.ToListAsync());
        }


        public IActionResult CustomerOrderConfirmation(string fullname, int tickets, int totalprice, Guid id, string time, string salon, string seats, string actualSeats)
        {
            ViewBag.fullname = fullname;
            ViewBag.tickets = tickets;
            ViewBag.id = id;
            ViewBag.totalprice = totalprice;
            ViewBag.time = time;
            ViewBag.salon = salon;
            ViewBag.actualSeats = actualSeats;

            return View();
        }

        public async Task<IActionResult> Checkout(string creditCard, string expiryDate, string cvc, string movie, string fullname, string email, string numberOfTickets, string totalPrice,string time, string salon, string actualSeats)
        {
            newOrder = new Orders
            {
                Name = fullname,
                Email = email,
                Id = Guid.NewGuid(),
                CreditCardNumber = creditCard,
                ExpiryDate = expiryDate,
                Cvc = cvc,
                NumberOfSeats = Int32.Parse(numberOfTickets),
                TotalPrice = Int32.Parse(totalPrice),
                Movie = movie
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerOrderConfirmation", "Orders", new { fullname = fullname, tickets = numberOfTickets, totalprice = newOrder.TotalPrice, id = newOrder.Id, time = time, salon = salon, actualSeats = actualSeats });

        }

        public IActionResult Payment(string fullname, string email, string entities, string NoOfTickets, string movie, string price, string time, string salon, string actualSeats)
        {
            List<string> numbers = new List<string>(entities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));


            if (numbers.Count() > 12)
            {
                throw new ArgumentNullException();
            }

            ViewBag.fullname = fullname;
            ViewBag.email = email;
            ViewBag.entities = entities;
            ViewBag.noTickets = NoOfTickets;
            ViewBag.movie = movie;
            ViewBag.price = price;
            ViewBag.time = time;
            ViewBag.salon = salon;
            ViewBag.actualSeats = actualSeats; 


            return View();
        }
    }
}
