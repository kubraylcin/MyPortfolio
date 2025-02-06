using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.AdminStatisticComponent
{
	public class _AdminStatisticFirst : ViewComponent
	{
		private readonly MyPortfolioContext _context;

		public _AdminStatisticFirst(MyPortfolioContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			ViewBag.v1 = _context.Skills.Count();
			ViewBag.v2 = _context.Messages.Count();//mesaj sayfasındaki toplam mesaj
			ViewBag.v3 = _context.Messages.Where(x => x.IsRead == false).Count(); //okunmamış mesajların sayısı
			ViewBag.v4 = _context.Messages.Where(x => x.IsRead == true).Count(); //okunmuşş mesajların sayısı
			ViewBag.v5 = _context.Testimonials.Count();
			ViewBag.v6 = _context.Experiences.Count();
			ViewBag.v7 = _context.Portfolios.Count();
			ViewBag.v8 = _context.ToDoLists.Count();
			return View();
		}
	}
}
