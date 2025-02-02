using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _PortfolioComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _PortfolioComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() {
            var portfolios = _context.Portfolios.Include(i => i.Image).ToList();
            return View(portfolios);
        }
    }
}
