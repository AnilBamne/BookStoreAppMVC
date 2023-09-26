using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAppWithMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL) 
        {
            this.bookBL = bookBL;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookModel bookModel)
        {
            var result=bookBL.AddBook(bookModel);
            return View(result);
        }
    }
}
