using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using System;

namespace MyPortfolio.ViewComponents
{
    public class FeatureSocialMedia : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public FeatureSocialMedia(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }
    }
}
