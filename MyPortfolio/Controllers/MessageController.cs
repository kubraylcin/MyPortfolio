using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    public class MessageController : Controller
    {
        private readonly MyPortfolioContext _context;

        public MessageController(MyPortfolioContext context)
        {
            _context = context;
        }

        // Gelen mesajları listelemek için
        public IActionResult Inbox()
        {
            var values = _context.Messages.ToList();
            return View(values);
        }

        public IActionResult ChangeIsReadToTrue(int id) // Mesaji okundu olarak isaretlemek icin
        {
            var value = _context.Messages.Find(id);
            value.IsRead = true;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }

        public IActionResult ChangeIsReadToFalse(int id) // Mesaji okunmadi olarak isaretlemek icin
        {
            var value = _context.Messages.Find(id);
            value.IsRead = false;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }
        public IActionResult MessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            return View(value);
        }

    }
}
