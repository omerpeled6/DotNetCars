using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend.Data;
using backend.Models;

namespace backend.Pages.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteUserModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return Unauthorized();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToPage("/Users/UsersList");
        }
    }
}