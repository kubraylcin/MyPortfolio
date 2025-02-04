using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _FeatureComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _FeatureComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Features.Include(i => i.Image).FirstOrDefault(); // One cikan alan icin sadece bir tane kayit olmasi gerektigi icin ilk kaydi view tarafina gonderiyoruz.
            ViewBag.imageFileName = value.Image.FileName;
            return View(value);
        }
    }
}
