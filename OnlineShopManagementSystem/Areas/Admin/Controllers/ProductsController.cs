using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewData["productTypeId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_dbContext.Tags.ToList(), "Id", "Name");
            return View();
        }
        //Post Method Create
        [HttpPost]
        public async Task<IActionResult>Create(Product products,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (image != null)
                {
                    var filename = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", image.FileName);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    //string uploadsFolder = Path.Combine(_environment.WebRootPath, "Images");
                    //uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //using (var fileStream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    image.CopyTo(fileStream);
                    //}

                    //var name = Path.Combine(_environment.WebRootPath + $@"\Images", Path.GetFileName(image.FileName));               
                    //await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    if (image == null)
                    {
                        products.Image = "img/noimage.png";
                    }
                    products.Image = "img/" + image.FileName;
                }
                _dbContext.Add(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_dbContext.Tags.ToList(), "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.Tag).FirstOrDefault(c => c.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
