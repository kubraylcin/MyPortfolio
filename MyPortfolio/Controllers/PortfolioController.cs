using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Helpers.Images;
using MyPortfolio.Models;

namespace MyPortfolio.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PortfolioController : Controller
    {
        private readonly MyPortfolioContext _context;
        private readonly IImageHelper _imageHelper;

        // Combine the two constructors into one
        public PortfolioController(MyPortfolioContext context, IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public IActionResult Index()
        {
            var portfolios = _context.Portfolios.Include(i => i.Image).ToList();
            return View(portfolios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PortfolioAddViewModel viewModel)
        {
            if (viewModel.Image != null) // If image is selected
            {
                // Handle image upload
                var imageUpload = await _imageHelper.Upload(viewModel.Title, viewModel.Image);
                Image image = new(imageUpload.FullName, viewModel.Image.ContentType);
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();

                // Entity Constructure sayesinde resmiyle beraber PortfolYO olusturdum
                Portfolio portfolio = new Portfolio
                {
                    Title = viewModel.Title,
                    SubTitle = viewModel.SubTitle,
                    Description = viewModel.Description,
                    Url = viewModel.Url,
                    ImageId = image.Id
                };
                await _context.Portfolios.AddAsync(portfolio);
                await _context.SaveChangesAsync();
            }
            else // If no image is selected
            {
                var portfolio = new Portfolio //Resim haricindeki alanlari Portfolio nesnesine aktar
                {
                    Title = viewModel.Title,
                    SubTitle = viewModel.SubTitle,
                    Description = viewModel.Description,
                    Url = viewModel.Url,
                };
                await _context.Portfolios.AddAsync(portfolio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int portfolioId)
        {
            var value = _context.Portfolios.Include(i => i.Image).FirstOrDefault(x => x.PortfolioId == portfolioId);
            

            if (value == null)
            {
                return NotFound();
            }

            var viewModel = new PortfolioUpdateViewModel()
            {
                Id = value.PortfolioId,
                Title = value.Title,
                SubTitle = value.SubTitle,
                Description = value.Description,
                Url = value.Url,
                ImageId = value.ImageId,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PortfolioUpdateViewModel viewModel)
        {
            var portfolio = await _context.Portfolios.Include(i => i.Image).FirstOrDefaultAsync(x => x.PortfolioId == viewModel.Id);
            if (portfolio == null)
            {
                return NotFound();
            }

            if (viewModel.Photo != null) // If a new image is selected
            {
                if (viewModel.ImageId != null) // If the portfolio has an existing image, delete it first
                {
                    _imageHelper.Delete(portfolio.Image.FileName); // Delete existing image
                }

                // Upload new image
                var imageUpload = await _imageHelper.Upload(viewModel.Title, viewModel.Photo);
                Image image = new(imageUpload.FullName, viewModel.Photo.ContentType);
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();

                portfolio.ImageId = image.Id;
            }

            // Update portfolio details
            portfolio.Title = viewModel.Title;
            portfolio.SubTitle = viewModel.SubTitle;
            portfolio.Description = viewModel.Description;
            portfolio.Url = viewModel.Url;
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int portfolioId)
        {
            var value = _context.Portfolios.Find(portfolioId);
            if (value != null)
            {
                _context.Portfolios.Remove(value);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
