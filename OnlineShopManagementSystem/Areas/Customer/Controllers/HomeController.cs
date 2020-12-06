using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShopManagementSystem.Data;
using OnlineShopManagementSystem.Models;

namespace OnlineShopManagementSystem.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _dbContext = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Products.Include(p=>p.ProductTypes).Include(s=>s.Tag).ToList());
        }

        //Get product detail action method
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
