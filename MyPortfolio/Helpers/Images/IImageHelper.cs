using MyPortfolio.Models;

namespace MyPortfolio.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadedModel> Upload(string name, IFormFile imageFile);
        void Delete(string imageName);
    }
}
