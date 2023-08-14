using hospital_management_system.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
/*using Microsoft.AspNetCore.Authentication.Cookies; */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) 
    .AddCookie(option => {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        

});
*/

// Register
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});
    
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

//
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

 pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
