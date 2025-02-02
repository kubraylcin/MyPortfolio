using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System;

namespace MyPortfolio.Controllers
{
    public class FeatureController : Controller
    {
        private readonly MyPortfolioContext _context;

        public FeatureController(MyPortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Ana sayfada one cikan alan yazisi icin
            var feature = _context.Features.FirstOrDefault();
            ViewBag.featureId = feature.FeatureId;
            return View(feature);
        }
        [HttpGet]
        public IActionResult Update(int featureId)
        {
            var feature = _context.Features.Find(featureId);
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(Feature feature)
        {
            _context.Features.Update(feature);
            _context.SaveChanges();
            return RedirectToAction("Index", "Feature");
        }
    }
}
