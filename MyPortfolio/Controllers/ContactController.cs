using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ContactController : Controller  // `Controller` miras almalı
    {
        private readonly MyPortfolioContext _context;

        public ContactController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contact = _context.Contacts.FirstOrDefault();
            if (contact != null)
            {
                ViewBag.contactId = contact.ContactId;
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult Update(int contactId)
        {
            var contact = _context.Contacts.Find(contactId);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }
    }
}
