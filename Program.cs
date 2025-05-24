using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/api/users/register", async (HttpContext context, AppDbContext db) =>
{
    var user = await context.Request.ReadFromJsonAsync<User>();
    if (user == null)
        return Results.BadRequest("Invalid user data");

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/api/users/login", async (HttpContext context, AppDbContext db) =>
{
    var login = await context.Request.ReadFromJsonAsync<User>();
    if (login == null)
        return Results.BadRequest("Invalid data");

    var user = await db.Users
        .FirstOrDefaultAsync(u => u.UserName == login.UserName && u.Password == login.Password);

    if (user == null)
        return Results.Unauthorized();

    context.Session.SetString("Username", user.UserName);
    context.Session.SetString("Role", user.Role ?? "User");
    return Results.Ok();
});

app.Run();