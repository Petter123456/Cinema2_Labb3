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

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create(string name, int price)
        {
            ViewBag.name = name;
            ViewBag.price = price;

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,CreditCardNumber,ExpiryDate,Cvc,NumberOfSeats,TotalPrice,Movie")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.Id = Guid.NewGuid();
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,CreditCardNumber,ExpiryDate,Cvc,NumberOfSeats,TotalPrice,Movie")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        public IActionResult CustomerOrderConfirmation(string customerName, int tickets, int totalprice, Guid id)
        {
            ViewBag.customerName = customerName;
            ViewBag.tickets = tickets;
            ViewBag.id = id;
            ViewBag.totalprice = totalprice;


            return View();

        }

        public async Task<IActionResult> Checkout(string creditCard, string expiryDate, string cvc, string movie, string fullname, string email, int numberOfTickets, int totalPrice)
        {
            newOrder = new Orders
            {
                Name = fullname,
                Email = email,
                Id = Guid.NewGuid(),
                CreditCardNumber = creditCard,
                ExpiryDate = expiryDate,
                Cvc = cvc,
                NumberOfSeats = numberOfTickets,
                TotalPrice = totalPrice,
                Movie = movie
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerOrderConfirmation", "Orders", new { customerName = fullname, tickets = numberOfTickets, totalprice = totalPrice, id = newOrder.Id });

        }

        public IActionResult Payment(string movie, string fullname, string email, int price, int numberOfTicket, int totalPrice)
        {
           

            ViewBag.movie = movie;
            ViewBag.fullname = fullname;
            ViewBag.email = email;
            ViewBag.price = price;
            ViewBag.numberOfTickets = numberOfTicket;
            ViewBag.totalPrice = totalPrice;

            return View();
        }
    }
}
