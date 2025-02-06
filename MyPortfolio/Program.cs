using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Data layer extension yüklenmesi
builder.Services.LoadDataLayerExtension(builder.Configuration); // ImageHelper için DI çerçevesi

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MyPortfolioContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session kullanýmýný ekleyelim
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/AdminLogin/Index"; // Giriþ yapýlmadýðýnda yönlendirme yapýlacak sayfa
		options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 60 dakika sonra otomatik çýkýþ yapacak
		options.SlidingExpiration = true; // Oturum süresi dolmadan iþlem yapýldýðýnda yenilenmesi
		options.Cookie.HttpOnly = true; // Cookie sadece HTTP üzerinden eriþilebilir
		options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS ile ayný sayfa üzerinde çalýþacak
	});

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Eðer HTTPS ile çalýþýyorsanýz
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Login olduktan 60 dakika sonra çýkýþ
	options.LoginPath = "/AdminLogin/Index"; // Giriþ yapýlmadýðýnda bu sayfaya yönlendirilecek
	options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts(); // Güvenli baðlantý için HSTS (HTTP Strict Transport Security) kullanýmý
}

app.UseHttpsRedirection(); // HTTPS yönlendirmesi
app.UseStaticFiles(); // Statik dosyalarýn sunulmasý

// Session'ý kullanabilmek için önce UseSession() çaðrýlmalý
app.UseSession();
app.UseRouting();

app.UseAuthentication(); // Kimlik doðrulama iþlemleri
app.UseAuthorization();  // Yetkilendirme iþlemleri

// Default controller route ayarý
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
