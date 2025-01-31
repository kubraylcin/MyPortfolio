using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
	public class ExperienceController : Controller
	{
		private readonly MyPortfolioContext _context;

		public ExperienceController(MyPortfolioContext context)
		{
			_context = context;
		}

		public IActionResult ExperienceList()
		{
			var experiences = _context.Experiences.ToList();
			return View(experiences);
		}
	}
}
