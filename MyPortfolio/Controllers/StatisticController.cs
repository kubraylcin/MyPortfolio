using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class StatisticController : Controller
	{ 
		public IActionResult Index()
		{
			

			return View();
		}
	}
}
