using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
	public class ExperienceController : Controller
	{
		private readonly MyPortfolioContext _context;

		public ExperienceController(MyPortfolioContext context)
		{
			_context = context;
		}

		// Tüm deneyimleri listeleyen sayfa
		public IActionResult ExperienceList()
		{
			var experiences = _context.Experiences.ToList();
			return View(experiences);
		}

		// Yeni deneyim ekleme formunu açar
		[HttpGet]
		public IActionResult CreateExperience()
		{
			return View();
		}

		// Yeni deneyimi veritabanına ekler butona tıklandığında çalışır
		[HttpPost]
		public IActionResult CreateExperience(Experience experience)
		{
			if (ModelState.IsValid) // Formun doğruluğunu kontrol et
			{
				_context.Experiences.Add(experience); // Burada `context` yerine `_context` kullandım
				_context.SaveChanges();
				return RedirectToAction("ExperienceList");
			}
			return View(experience); // Hata varsa aynı sayfaya geri dön
		}
	}
}
