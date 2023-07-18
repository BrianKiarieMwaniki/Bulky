using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet(int? id)
        {
            if (id == null && id != 0) return;

            Category = db.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPost()
        {
            Category? obj = db.Categories.Find(Category.Id);
            if (obj == null) return NotFound();

            db.Categories.Remove(obj);
            db.SaveChanges();
            return RedirectToPage("index");
        }
    }
}
