using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.LoadDataLayerExtension(builder.Configuration); // ImageHelper i�in DI cercevesi

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;
builder.Services.AddDbContext<MyPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddSession();
builder.Services.AddAuthentication(
	  CookieAuthenticationDefaults.AuthenticationScheme)
		  .AddCookie(x =>
		  {
			  x.LoginPath = "/AdminLogin/Index";  // Giris yapmadiginda bu sayfaya yonlendirecek
		  }
	  );
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60); //siteye login olduktan 60 dk sonra otomatik cikis yapar
	options.LoginPath = "/AdminLogin/Index/";
	options.SlidingExpiration = true;
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
