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
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.Tag).Where(p => p.Price >= lowAmount && p.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                 products = _dbContext.Products.Include(c => c.ProductTypes).Include(p => p.Tag).ToList();
            }
            return View(products);
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
                var searchProduct = _dbContext.Products.FirstOrDefault(P => P.Name == products.Name);
                if (searchProduct!=null)
                {
                    ViewBag.message = "This product is already exists";
                    ViewData["productTypeId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_dbContext.Tags.ToList(), "Id", "Name");
                    return View(products);
                }

                if (image != null)
                {
                    var filename = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", image.FileName);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
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
        [HttpPost]
        public async Task<IActionResult>Edit(Product products,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var filename = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", image.FileName);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    if (image == null)
                    {
                        products.Image = "img/noimage.png";
                    }
                    products.Image = "img/" + image.FileName;
                }
                _dbContext.Update(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(p => p.ProductTypes).Include(s => s.Tag).FirstOrDefault(m => m.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
        }
        // delete get method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(p => p.ProductTypes).Include(s => s.Tag).Where(m => m.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult>Delete(int id)
        {
            if (id==0)
            {
                return NotFound(id);
               
            }
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
