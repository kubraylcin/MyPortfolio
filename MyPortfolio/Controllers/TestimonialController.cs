using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class TestimonialController : Controller
    {
        private readonly MyPortfolioContext _context;

        public TestimonialController(MyPortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Create(Testimonial testimonial, IFormFile ImageFile)
		{
			if (ImageFile != null && ImageFile.Length > 0)
			{
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

				using (var stream = new FileStream(path, FileMode.Create))
				{
					ImageFile.CopyTo(stream);
				}

				testimonial.ImageUrl = "/images/" + fileName;
			}
			else
			{
				testimonial.ImageUrl = "/images/asdasd_723.png"; // Görsel yüklenmezse varsayılan görsel
			}

			_context.Testimonials.Add(testimonial);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
        public IActionResult Update(int testimonialId)
        {
            var value = _context.Testimonials.Find(testimonialId);
            return View(value);
        }
        [HttpPost]
        public IActionResult Update(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int testimonialId)
        {
            var value = _context.Testimonials.Find(testimonialId);
            _context.Testimonials.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
