using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.AdminStatisticComponent
{
	public class _AdminStatisticMessages:ViewComponent
	{
		private readonly MyPortfolioContext _context;

		public _AdminStatisticMessages(MyPortfolioContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var messages= _context.Messages.Where(x=>x.IsRead==false).ToList(); //okunmamış mesajlar
			return View(messages);
		}
	}
}
