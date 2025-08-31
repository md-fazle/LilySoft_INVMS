using LilySoft_INVMS.Auth.Services;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Configuration for DbContext
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext with SQL Server provider
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

// Register AuthServices
builder.Services.AddScoped<AuthServices>();

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";           // Redirect to login if unauthorized
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Redirect to access denied page
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

// Add authorization policies based on permissions
builder.Services.AddAuthorization(options =>
{
    // Hardcoded example, can be loaded dynamically from DB if needed
    var permissions = new[]
    {
        "ManageUsers", "ViewUsers", "ManageRoles", "ManagePermissions",
        "AddProduct", "EditProduct", "DeleteProduct", "ViewProducts",
        "ManageStock", "CreateOrder", "ApproveOrder", "ViewOrders",
        "ViewReports", "ExportReports"
    };

    foreach (var permission in permissions)
    {
        options.AddPolicy(permission, policy =>
            policy.RequireClaim("Permission", permission));
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication & authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

// Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
