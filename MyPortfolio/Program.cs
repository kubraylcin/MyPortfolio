using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using MyPortfolio.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// **Data Layer Extension Y�kleme**
builder.Services.LoadDataLayerExtension(builder.Configuration); // Burada DalExtensions ile ba�lant� sa�lan�r.

// **Kimlik do�rulama ve yetkilendirme ayarlar�**
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/AdminLogin/Index";  // Giri� yap�lmad���nda y�nlendirme yap�lacak sayfa
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 60 dakika sonra ��k��
    options.SlidingExpiration = true; // Oturum s�resi dolmadan i�lem yap�ld���nda yenilenmesi
    options.Cookie.HttpOnly = true; // Cookie yaln�zca HTTP �zerinden eri�ilebilir
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS ile �al���yorsa ayn� sayfa �zerinden ge�i�
});

// **Session kullan�m�**
builder.Services.AddSession();

// **MVC ve Razor deste�i**
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// **Hata yakalama ve g�venlik �nlemleri**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // G�venli ba�lant� i�in HSTS (HTTP Strict Transport Security) kullan�m�
}

app.UseHttpsRedirection(); // HTTPS y�nlendirmesi
app.UseStaticFiles(); // Statik dosyalar�n sunulmas�
app.UseRouting();

app.UseSession();  // **Session kullan�m�**
app.UseAuthentication();  // **Kimlik do�rulama i�lemleri**
app.UseAuthorization();  // **Yetkilendirme i�lemleri**

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();  // **Uygulama ba�latma**
