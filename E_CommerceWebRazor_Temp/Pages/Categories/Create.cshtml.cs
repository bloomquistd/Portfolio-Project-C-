using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using E_CommerceWebRazor_Temp.Data;
using E_CommerceWebRazor_Temp.Models;

namespace E_CommerceWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
 
        public Category Category{ get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(Category obj)
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}