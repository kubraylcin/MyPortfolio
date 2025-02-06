using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class StatisticController : Controller
	{
		private readonly MyPortfolioContext _context;

		public StatisticController(MyPortfolioContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			

			return View();
		}
	}
}
