using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using System;

namespace MyPortfolio.ViewComponents
{
    public class ContactInfoComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public ContactInfoComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var contact = _context.Contacts.FirstOrDefault();
            return View(contact);
        }
    }
}
