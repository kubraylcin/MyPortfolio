using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
            services.AddDbContext<MyPortfolioContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
			services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<MyPortfolioContext>()
				.AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<MyPortfolioContext>();
			services.AddScoped<IImageHelper, ImageHelper>(); // Gorselleri Image tablosunda tutacagimiz icin Helper yazdik.
            return services;
        }
    }
}
