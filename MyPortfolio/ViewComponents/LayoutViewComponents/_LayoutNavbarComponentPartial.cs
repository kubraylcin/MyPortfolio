using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.LayoutViewComponents
{
	public class _LayoutNavbarComponentPartial:ViewComponent
	{
		private readonly MyPortfolioContext _context;

		public _LayoutNavbarComponentPartial(MyPortfolioContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			//yapılacak olanların toplam sayıısı
			ViewBag.toDolistCount=_context.ToDoLists.Where(x=>x.Status==false).Count();
			//yapılmamış bildirimleri getirmek için
			var values = _context.ToDoLists.Where(x => x.Status == false).ToList();
			return View(values);
		}
	}
}
