using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using E_CommerceWebRazor_Temp.Data;
using E_CommerceWebRazor_Temp.Models;

namespace E_CommerceWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}