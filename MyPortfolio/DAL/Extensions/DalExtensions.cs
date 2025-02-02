using Microsoft.AspNetCore.Razor.TagHelpers;
using MyPortfolio.Helpers.Images;

namespace MyPortfolio.DAL.Extensions
{
    public static class DalExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IImageHelper, ImageHelper>(); // Gorselleri Image tablosunda tutacagimiz icin Helper yazdik.
            return services;
        }
    }
}
