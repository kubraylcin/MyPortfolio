using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class UnreadMessageList:ViewComponent
    {
        private readonly MyPortfolioContext  _context;

        public UnreadMessageList(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            // Okunmamis mesajlar bildirim kisminda listelenecek.
            var unreadMessages = _context.Messages.Where(x => x.IsRead == false).ToList();
            ViewBag.UnreadMessagesCount = unreadMessages.Count;
            return View(unreadMessages);
        }
    }
}
