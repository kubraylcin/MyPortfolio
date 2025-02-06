using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Data layer extension y�klenmesi
builder.Services.LoadDataLayerExtension(builder.Configuration); // ImageHelper i�in DI �er�evesi

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MyPortfolioContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session kullan�m�n� ekleyelim
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/AdminLogin/Index"; // Giri� yap�lmad���nda y�nlendirme yap�lacak sayfa
		options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 60 dakika sonra otomatik ��k�� yapacak
		options.SlidingExpiration = true; // Oturum s�resi dolmadan i�lem yap�ld���nda yenilenmesi
		options.Cookie.HttpOnly = true; // Cookie sadece HTTP �zerinden eri�ilebilir
		options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS ile ayn� sayfa �zerinde �al��acak
	});

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // E�er HTTPS ile �al���yorsan�z
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Login olduktan 60 dakika sonra ��k��
	options.LoginPath = "/AdminLogin/Index"; // Giri� yap�lmad���nda bu sayfaya y�nlendirilecek
	options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts(); // G�venli ba�lant� i�in HSTS (HTTP Strict Transport Security) kullan�m�
}

app.UseHttpsRedirection(); // HTTPS y�nlendirmesi
app.UseStaticFiles(); // Statik dosyalar�n sunulmas�

// Session'� kullanabilmek i�in �nce UseSession() �a�r�lmal�
app.UseSession();
app.UseRouting();

app.UseAuthentication(); // Kimlik do�rulama i�lemleri
app.UseAuthorization();  // Yetkilendirme i�lemleri

// Default controller route ayar�
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
