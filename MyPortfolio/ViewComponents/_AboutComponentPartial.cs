using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _AboutComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _AboutComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.aboutTitle=_context.Abouts.Select(x => x.Title).ToList().FirstOrDefault();
            ViewBag.aboutDescription=_context.Abouts.Select(x=>x.SubDescription).ToList().FirstOrDefault();
            ViewBag.aboutDetail=_context.Abouts.Select(x=>x.Details).ToList().FirstOrDefault();
            return View();
        }
    }
}
