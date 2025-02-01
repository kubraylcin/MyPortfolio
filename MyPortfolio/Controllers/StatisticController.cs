using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Controllers
{
	public class StatisticController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
