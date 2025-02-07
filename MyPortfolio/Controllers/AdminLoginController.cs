using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Controllers
{
	public class AdminLoginController : Controller
	{
		private readonly MyPortfolioContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AdminLoginController(MyPortfolioContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

                if (result.Succeeded)
                {
                    var userId = _userManager.Users.Where(x => x.UserName == model.UserName).Select(y => y.Id).FirstOrDefault();
                    var userRoleId = _context.UserRoles.Where(x => x.UserId.Equals(userId)).Select(y => y.RoleId).FirstOrDefault();
                    var adminRoleId = _roleManager.Roles.Where(x => x.Name.Equals("Admin") || x.Name.Equals("admin")).Select(y => y.Id).FirstOrDefault();

                    if (userRoleId == adminRoleId)
                    {
                        return RedirectToAction("Index", "Statistic");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Admin rolüne sahip değil.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                }
            }

            // Return the model with validation errors if any
            return View(model);
        }



        public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Default");
		}
	}
}
