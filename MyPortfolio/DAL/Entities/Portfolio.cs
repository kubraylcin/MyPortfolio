namespace MyPortfolio.DAL.Entities
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }//tıklayınca görsele açıklama geliyor
    }
}
