using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopManagementSystem.Data;
using OnlineShopManagementSystem.Models;

namespace OnlineShopManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbContext;
        private IWebHostEnvironment _environment;

        
        public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            _environment = environment;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Products.Include(c=>c.ProductTypes).Include(s=>s.Tag).ToList());
        }
        //Get Method Create
        public ActionResult Create()
        {
            return View();
        }
        //Post Method Create
        [HttpPost]
        public async Task<ActionResult>Create(Product products,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image ="Images/" +image.FileName;
                }
                _dbContext.Add(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
