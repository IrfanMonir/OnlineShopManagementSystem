using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopManagementSystem.Data;
using OnlineShopManagementSystem.Models;

namespace OnlineShopManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.ProductTypes.ToList());
        }

        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateProduct(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductTypes.Add(productTypes);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
