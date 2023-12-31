﻿using BulkyWeb.Data;
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
                TempData["successMsg"] = "Category created successfully!";              
                return RedirectToAction("Index");
            }

            return View();
        }   
        
        public IActionResult Edit(int? id)
        {
            if (id is null && id == 0) return NotFound();

            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
               await _db.SaveChangesAsync();
                TempData["successMsg"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View();
        }
        
        public IActionResult Delete(int? id)
        {
            if(id is null  && id == 0) return NotFound();

            var category = _db.Categories.FirstOrDefault(c =>c.Id == id);

            if(category == null) return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["successMsg"] = "Category deleted successfully!";

            return RedirectToAction("Index");
        }
        private async Task AddCategory(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }
    }
}
