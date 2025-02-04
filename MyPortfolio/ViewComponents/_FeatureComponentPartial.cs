using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
	public class _FeatureComponentPartial : ViewComponent
	{
		private readonly MyPortfolioContext _context;

		public _FeatureComponentPartial(MyPortfolioContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			// İlk özellik kaydını ve ilişkili resmini veritabanından getiriyoruz.
			var value = _context.Features.Include(i => i.Image).FirstOrDefault();

			// value ve Image null değilse, resim adı alınır.
			if (value?.Image != null)
			{
				ViewBag.imageFileName = value.Image.FileName; // Resim dosya adını view tarafına gönderiyoruz.
			}
			else
			{
				ViewBag.imageFileName = string.Empty; // Resim yoksa boş bir değer gönderiyoruz.
			}

			return View(value); // Feature nesnesini (ve resmini) view tarafına gönderiyoruz.
		}
	}
}
