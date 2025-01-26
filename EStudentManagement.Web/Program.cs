using EStudentManagement.Web.Data;
using EStudentManagement.Web.Repositories.IRepositories;
using EStudentManagement.Web.Repositories.Repositories;
using EStudentManagement.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();  // Required for session
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set your desired session timeout
    options.Cookie.HttpOnly = true;  // Set cookies to be HTTP-only
    options.Cookie.IsEssential = true;  // Makes session cookies essential
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("StudentPortal")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
