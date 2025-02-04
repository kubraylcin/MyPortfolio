using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Models
{
    public class FeatureUpdateViewModel
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; } // Gorsel yukleme islemi bos da gecilebilir
        public Image Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}


