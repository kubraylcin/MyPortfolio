using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _ContactComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _ContactComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() {
            var contact=_context.Contacts.FirstOrDefault();
            ViewBag.title=contact.Title; 
            ViewBag.description=contact.Description;
            return View(); 
        }
    }
}
