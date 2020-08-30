using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopManagementSystem.Data;

namespace OnlineShopManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Products.Include(c=>c.ProductTypes).Include(s=>s.Tag).ToList());
        }
    }
}
