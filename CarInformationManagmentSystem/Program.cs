using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework Core with SQL Server using the connection string.
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories as scoped services.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ICarTypeRepository, CarTypeRepository>();
builder.Services.AddScoped<ICarTransmissionTypeRepository, CarTransmissionTypeRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

// Configure authentication services.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
    });

// Configure authorization policies (if needed).
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Middleware components
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable authentication (must be placed before authorization).
app.UseAuthentication();

// Enable authorization.
app.UseAuthorization();

// Define routing patterns for controllers and actions.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}"); // Default route pattern

app.MapControllerRoute(
    name: "car",
    pattern: "{controller=Car}/{action=Index}/{model?}");

// Run the application.
app.Run();