using AspNetCoreHero.ToastNotification;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Connect database
var connectionString = builder.Configuration.GetConnectionString("dbMBVN");
builder.Services.AddDbContext<MBVNContext>(options => options.UseSqlServer(connectionString));

// Use session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10080);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the containers
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();

