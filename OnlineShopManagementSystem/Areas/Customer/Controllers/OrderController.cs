using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopManagementSystem.Data;
using OnlineShopManagementSystem.Models;
using OnlineShopManagementSystem.Utility;

namespace OnlineShopManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get checkout action method
        public IActionResult CheckOut()
        {
            return View();
        }


        //Post checkout action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>CheckOut(Order order)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products!=null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    order.OrderDetails.Add(orderDetails);
                }
            }
            order.OrderNo = GetOrderNo();
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Product>());
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _dbContext.Orders.ToList().Count + 1;
            return rowCount.ToString("000");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
