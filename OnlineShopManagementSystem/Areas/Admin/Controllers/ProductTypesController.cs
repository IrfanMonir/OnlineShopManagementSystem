﻿using System;
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
                TempData["save"] = "Product type has saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        public ActionResult ProductEdit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductTypes.Update(productTypes);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetails(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ProductDelete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDelete(int? id,ProductTypes productTypes)
        {
            if (id==null)
            {
                return NotFound();
            }

            if (id!= productTypes.Id)
            {
                return NotFound();
            }
            var productType = _dbContext.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.ProductTypes.Remove(productType);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);

        }
    }
}
