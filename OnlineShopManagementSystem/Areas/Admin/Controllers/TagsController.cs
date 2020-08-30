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
    public class TagsController : Controller
    {
       
        private ApplicationDbContext _dbContext;
        public TagsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Tags.ToList());
        }
        public ActionResult CreateTag()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTag(Tag tags)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tags.Add(tags);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Tag has saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }
        public ActionResult TagEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagEdit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tags.Update(tag);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
        public ActionResult TagDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TagDetails(Tag tag)
        {
            return RedirectToAction(nameof(Index));
        }
        public ActionResult TagDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TagDelete(int? id, Tag tags)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != tags.Id)
            {
                return NotFound();
            }
            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.Tags.Remove(tag);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(tags);

        }
    }
}
