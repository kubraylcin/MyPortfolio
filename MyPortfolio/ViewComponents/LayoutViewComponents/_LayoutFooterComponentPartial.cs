using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.ViewComponents.LayoutViewComponents
{
    public class _LayoutFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
