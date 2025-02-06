using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.AdminStatisticComponent
{
	public class _AdminStatisticToDoList:ViewComponent
	{
		private readonly MyPortfolioContext _context;

		public _AdminStatisticToDoList(MyPortfolioContext context)
		{
			_context = context;
		}public IViewComponentResult Invoke()
		{
			var toDoLists = _context.ToDoLists.Where(x => x.Status == false).ToList(); // Yapilmamis gorevler
			return View(toDoLists);
		}
	
	}
}
