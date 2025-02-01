using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly MyPortfolioContext _context;

        public ToDoListController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.ToDoLists.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateToDoList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateToDoList(ToDoList toDoList)
        {
            _context.ToDoLists.Add(toDoList);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteToDoList(int id)
        {
            var value = _context.ToDoLists.Find(id);
            if (value != null)
            {
                _context.ToDoLists.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // *** Update İşlemi ***

        // 1. Güncellenecek veriyi formda göstermek için GET metodu
        [HttpGet]
        public IActionResult UpdateToDoList(int id)
        {
            var value = _context.ToDoLists.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        // 2. Güncellenmiş veriyi kaydetmek için POST metodu
        [HttpPost]
        public IActionResult UpdateToDoList(ToDoList toDoList)
        {
            var value = _context.ToDoLists.Find(toDoList.ToDoListId);
            if (value != null)
            {
                value.Title = toDoList.Title;
                value.ImageUrl = toDoList.ImageUrl;
                value.Date = toDoList.Date;
                value.Status = toDoList.Status;

                _context.ToDoLists.Update(value);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoList);
        }
		public IActionResult ChangeToDoListToTrue(int id) // Görevi tamamlandı olarak işaretlemek için
		{
			var value = _context.ToDoLists.Find(id);
			value.Status = true;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult ChangeToDoListToFalse(int id) // Görevi tamamlanmadı olarak işaretlemek için
		{
			var value = _context.ToDoLists.Find(id);
			value.Status = false;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
