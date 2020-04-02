using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema_Labb3.Models;
using Nancy.Json;

namespace Cinema2_Labb3.Controllers
{
    public class MoviesController : Controller
    {
        private readonly BerrasBiografContext _context;

        public MoviesController(BerrasBiografContext context)
        {
            _context = context;
        }

     
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var firstViewingPaccinoSalon = 0;
            var secondViewingPaccinoSalon = 0;
            var thirdViewingPaccinoSalon = 0;

            var contextPaccinoSalon = await _context.PaccinoSalon.ToListAsync();

            foreach (var item in contextPaccinoSalon)
            {
                if (item.Availible == true && item.Id < 51)
                {
                    firstViewingPaccinoSalon = firstViewingPaccinoSalon + 1; 
                } else if(item.Availible && item.Id > 50 && item.Id < 101)
                {
                    secondViewingPaccinoSalon = secondViewingPaccinoSalon + 1;
                }
                else if (item.Availible && item.Id > 100)
                {
                    thirdViewingPaccinoSalon = thirdViewingPaccinoSalon + 1; 
                }
            }

            ViewBag.firstViewingPaccinoSalon = firstViewingPaccinoSalon;
            ViewBag.secondViewingPaccinoSalon = secondViewingPaccinoSalon;
            ViewBag.thirdViewingPaccinoSalon = thirdViewingPaccinoSalon;

            var firstViewingDeNiroSalon = 0;
            var secondViewingDeNiroSalon = 0;
            var thirdViewingDeNiroSalon = 0;

            var contextDeNiroSalon = await _context.DeNiroSalon.ToListAsync();

            foreach (var item in contextDeNiroSalon)
            {
                if (item.Availible == true && item.Id < 101)
                {
                    firstViewingDeNiroSalon = firstViewingDeNiroSalon + 1;
                }
                else if (item.Availible && item.Id > 100 && item.Id < 201)
                {
                    secondViewingDeNiroSalon = secondViewingDeNiroSalon + 1;
                }
                else if (item.Availible && item.Id > 200)
                {
                    thirdViewingDeNiroSalon = thirdViewingDeNiroSalon + 1;
                }
            }

            ViewBag.firstViewingDeNiroSalon = firstViewingDeNiroSalon;
            ViewBag.secondViewingDeNiroSalon = secondViewingDeNiroSalon;
            ViewBag.thirdViewingDeNiroSalon = thirdViewingDeNiroSalon;


            return View(await _context.Movies.ToListAsync());
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        public IActionResult ConfirmOrder(string entities, string NoOfTickets, string movie, string price, string time)
        {
            
            ViewBag.tickets = NoOfTickets;
            ViewBag.seats = entities;
            ViewBag.movie = movie;
            ViewBag.price = price;
            ViewBag.time = time;


            return View();
        }
    }
}
