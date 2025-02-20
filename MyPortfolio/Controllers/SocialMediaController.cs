﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class SocialMediaController : Controller
	{
		private readonly MyPortfolioContext _context;

		public SocialMediaController(MyPortfolioContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var values = _context.SocialMedias.ToList();
			return View(values);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(SocialMedia socialMedia)
		{
			_context.SocialMedias.Add(socialMedia);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Update(int socialMediaId)
		{
			var value = _context.SocialMedias.Find(socialMediaId);
			return View(value);
		}
		[HttpPost]
		public IActionResult Update(SocialMedia socialMedia)
		{
			_context.SocialMedias.Update(socialMedia);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int socialMediaId)
		{
			var value = _context.SocialMedias.Find(socialMediaId);
			_context.SocialMedias.Remove(value);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
