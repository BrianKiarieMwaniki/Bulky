using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet(int? id)
        {
            if (id == null && id == 0) return;

            var result = db.Categories.FirstOrDefault(c => c.Id == id);

            if (result == null) return;

            Category = result;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(Category);
                db.SaveChanges();

                TempData["success"] = "Category updated successfully!";

                return RedirectToPage("index");
            }

            return Page();
        }
    }
}
