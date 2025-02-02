using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _TestimonialComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _TestimonialComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public  IViewComponentResult Invoke()
        {
            var testimonials = _context.Testimonials.ToList();
            return View(testimonials);
        }
    }
}
