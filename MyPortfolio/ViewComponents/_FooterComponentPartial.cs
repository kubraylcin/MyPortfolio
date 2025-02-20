﻿using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _FooterComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _FooterComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }
    }
}
