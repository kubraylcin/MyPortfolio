using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System;

namespace MyPortfolio.Controllers
{
    public class AboutController : Controller
    {
        private readonly MyPortfolioContext _context;

        public AboutController(MyPortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var about = _context.Abouts.FirstOrDefault();
            ViewBag.aboutId = about.AboutId;
            return View(about);
        }
        [HttpGet]
        public IActionResult Update(int aboutId)
        {
            var about = _context.Abouts.Find(aboutId);
            return View(about);
        }
        [HttpPost]
        public IActionResult Update(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return RedirectToAction("Index", "About");
        }
    }
}
