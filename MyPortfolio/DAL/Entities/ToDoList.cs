namespace MyPortfolio.DAL.Entities
{
    public class ToDoList
    {
        public int ToDoListId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }// Icon bilgisi (la la-home gibi)
        public DateTime Date { get; set; }
        public bool Status { get; set; }

    }
}
