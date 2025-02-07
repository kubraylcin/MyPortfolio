using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Helpers.Images;
using System;

namespace MyPortfolio.DAL.Extensions
{
    public static class DalExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            // **Veritabanı Bağlantısı**
            services.AddDbContext<MyPortfolioContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // **ASP.NET Identity Yapılandırması**
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<MyPortfolioContext>()
                .AddDefaultTokenProviders();

            // **Helper Sınıflarının Bağımlılık Enjeksiyonu**
            services.AddScoped<IImageHelper, ImageHelper>();  // Görseller için helper

            return services;
        }
    }
}
