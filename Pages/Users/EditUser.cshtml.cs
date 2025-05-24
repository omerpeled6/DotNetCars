using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend.Data;
using backend.Models;

namespace backend.Pages.Users
{
    public class EditUserModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditUserModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User? UserToEdit { get; set; }

        public bool IsAdmin { get; set; } = false;

        public IActionResult OnGet(int id)
        {
            IsAdmin = HttpContext.Session.GetString("Role") == "Admin";
            if (!IsAdmin)
                return Unauthorized();

            UserToEdit = _context.Users.FirstOrDefault(u => u.Id == id);
            if (UserToEdit == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            IsAdmin = HttpContext.Session.GetString("Role") == "Admin";
            if (!IsAdmin || UserToEdit == null)
                return Unauthorized();

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == UserToEdit.Id);
            if (existingUser == null)
                return NotFound();

            // עדכון כל השדות
            existingUser.UserName = UserToEdit.UserName;
            existingUser.Password = UserToEdit.Password;
            existingUser.FirstName = UserToEdit.FirstName;
            existingUser.LastName = UserToEdit.LastName;
            existingUser.Email = UserToEdit.Email;
            existingUser.Phone = UserToEdit.Phone;
            existingUser.BirthDate = UserToEdit.BirthDate;
            existingUser.Gender = UserToEdit.Gender;
            existingUser.FavoriteCar = UserToEdit.FavoriteCar;
            existingUser.Role = UserToEdit.Role;

            _context.SaveChanges();
            return RedirectToPage("/Users/UsersList");
        }
    }
}