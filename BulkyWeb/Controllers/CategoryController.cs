using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name?.ToLower() == "test")
                ModelState.AddModelError("", "Test is invalid value");

            if (ModelState.IsValid)
            {
                await AddCategory(category);
                return RedirectToAction("Index");
            }

            return View();
        }        
        private async Task AddCategory(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }
    }
}
