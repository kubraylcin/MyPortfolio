using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Extensions;
using MyPortfolio.Models;
using System.Security.Claims;

namespace MyPortfolio.ViewComponents.LayoutViewComponents
{
	public class _LayoutSidebarComponentPartial:ViewComponent
	{
        private readonly MyPortfolioContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public _LayoutSidebarComponentPartial(MyPortfolioContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }
        public IViewComponentResult Invoke()
		{
            var userId = _user.GetLoggedInUserId(); // Giris yapan kisinin id'si
            var user = _context.Users.Include(i => i.Image).Where(x => x.Id == userId).FirstOrDefault();
            ViewBag.NameSurname = user.Name + " " + user.Surname;
            ViewBag.UserName = user.UserName;
            ViewBag.userImageId = user.ImageId;
            if (user.ImageId is not null)
                ViewBag.UserImage = user.Image.FileName;
            return View();
        }
    }
}
