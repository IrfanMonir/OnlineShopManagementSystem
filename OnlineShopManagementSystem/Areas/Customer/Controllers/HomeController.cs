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
using OnlineShopManagementSystem.Utility;
using X.PagedList;

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

        public IActionResult Index(int?page)
        {
            return View(_dbContext.Products.Include(p=>p.ProductTypes).Include(s=>s.Tag).ToList().ToPagedList(page??1,9));
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
        //Post product detail action method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Product>>("products");
            if (products==null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
            //return View(product);
        }
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                }
            }
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products!=null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product!=null)
                {
                    products.Remove(product);
                }
            }
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        //Get product cart action method
        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products==null)
            {
                products = new List<Product>();
            }
            return View(products);
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
