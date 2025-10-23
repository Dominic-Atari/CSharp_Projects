using Dominic.Net.Models;
using Microsoft.EntityFrameworkCore;
// See https://aka.ms/new-console-template for more information
var builder = WebApplication.CreateBuilder(args);

// Add services to the container, main reason is to enable MVC.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession(); // Add session support
builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor support

//  DATABASE ExTENSION
// Configure the DbContext to use MySQL with the connection string from appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DominicsShopDbContextConnectionString");
builder.Services.AddDbContext<DominicShopDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.32-mysql"))
);

//  DEPENDENCY INJECTION CONFIGURATION
// Register the pie repository service for dependency injection.
builder.Services.AddScoped<IPieRepository, PieRepository>();
// Register the category repository service for dependency injection.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


// Build the app.
var app = builder.Build();

// Configure middleware to serve static files.
app.UseStaticFiles(); // Enable serving static files from wwwroot folder.
app.UseSession(); // Enable session middleware.
app.UseDeveloperExceptionPage(); // Enable detailed error pages in development.

// Enable routing.
//app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute(); // Map default controller route.
DbInitializer.Seed(app); // Seed the database with initial data.
// Configure the HTTP request pipeline.
app.Run();