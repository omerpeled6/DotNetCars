using Microsoft.AspNetCore.Mvc.RazorPages;
using backend.Models;
using backend.Data;

namespace backend.Pages.Users
{
    public class UsersListModel : PageModel
    {
        private readonly AppDbContext _context;

        public UsersListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<User> Users { get; set; } = new();
        public bool IsAdmin { get; set; } = false;
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }

        public void OnGet(string? search, string? sortBy)
        {
            var role = HttpContext.Session.GetString("Role");
            IsAdmin = role == "Admin";

            if (!IsAdmin) return;

            SearchTerm = search;
            SortBy = sortBy;

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u =>
                    u.UserName!.Contains(search) ||
                    u.FirstName!.Contains(search) ||
                    u.LastName!.Contains(search));
            }

            query = sortBy switch
            {
                "birth" => query.OrderBy(u => u.BirthDate),
                "name" => query.OrderBy(u => u.FirstName),
                _ => query.OrderBy(u => u.UserName)
            };

            Users = query.ToList();
        }
    }
}