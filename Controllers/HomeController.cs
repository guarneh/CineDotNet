﻿using CIneDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CIneDotNet.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;

        

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string busqueda)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetInt32("id") != 0)
            {
                
                
                if (busqueda != null)
                {
                    var myContext = _context.peliculas.Where(p => p.Nombre.Equals(busqueda));
                    return View(await myContext.ToListAsync());
                }
                else 
                {
                    return View(await _context.peliculas.ToListAsync());
                }
                    
            }
            else
            {
                return RedirectToAction("index","login");
            }
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}