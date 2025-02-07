using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using MyPortfolio.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// **Data Layer Extension Yükleme**
builder.Services.LoadDataLayerExtension(builder.Configuration); // Burada DalExtensions ile baðlantý saðlanýr.

// **Kimlik doðrulama ve yetkilendirme ayarlarý**
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/AdminLogin/Index";  // Giriþ yapýlmadýðýnda yönlendirme yapýlacak sayfa
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 60 dakika sonra çýkýþ
    options.SlidingExpiration = true; // Oturum süresi dolmadan iþlem yapýldýðýnda yenilenmesi
    options.Cookie.HttpOnly = true; // Cookie yalnýzca HTTP üzerinden eriþilebilir
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS ile çalýþýyorsa ayný sayfa üzerinden geçiþ
});

// **Session kullanýmý**
builder.Services.AddSession();

// **MVC ve Razor desteði**
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// **Hata yakalama ve güvenlik önlemleri**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Güvenli baðlantý için HSTS (HTTP Strict Transport Security) kullanýmý
}

app.UseHttpsRedirection(); // HTTPS yönlendirmesi
app.UseStaticFiles(); // Statik dosyalarýn sunulmasý
app.UseRouting();

app.UseSession();  // **Session kullanýmý**
app.UseAuthentication();  // **Kimlik doðrulama iþlemleri**
app.UseAuthorization();  // **Yetkilendirme iþlemleri**

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();  // **Uygulama baþlatma**
