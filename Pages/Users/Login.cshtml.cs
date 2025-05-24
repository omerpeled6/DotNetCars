using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend.Data;
using backend.Models;

namespace backend.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User LoginUser { get; set; } = new();

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ErrorMessage = "";
        }

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == LoginUser.UserName && u.Password == LoginUser.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.UserName);
                HttpContext.Session.SetString("Role", user.Role ?? "User");
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Username or password is incorrect";
                return Page();
            }
        }
    }
}