using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _StatisticComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _StatisticComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() {
            ViewBag.ExperienceCount = _context.Experiences.Count();
            ViewBag.SkillCount = _context.Skills.Count();
            ViewBag.PortfolioCount = _context.Portfolios.Count(); // Tamamlanan proje sayisi
            ViewBag.TestimonialCount = _context.Testimonials.Count(); return View(); }
    }
}
