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
		// Deneyimi güncelleme formunu açar düncellenecek verileri id göre bulma
		[HttpGet]
		public IActionResult UpdateExperience(int id)
		{
			var experience = _context.Experiences.Find(id);
			if (experience == null)
			{
				return NotFound();
			}
			return View(experience);
		}

		// Deneyimi günceller ve kaydeder
		[HttpPost]
		public IActionResult UpdateExperience(Experience experience)
		{
			if (ModelState.IsValid)
			{
				_context.Experiences.Update(experience);
				_context.SaveChanges();
				return RedirectToAction("ExperienceList");
			}
			return View(experience);
		}
		// Belirtilen ID'ye sahip deneyimi siler
		public IActionResult DeleteExperience(int id)
		{
			var experience = _context.Experiences.Find(id);
			if (experience != null)
			{
				_context.Experiences.Remove(experience);
				_context.SaveChanges();
			}
			return RedirectToAction("ExperienceList");
		}
	}
}
