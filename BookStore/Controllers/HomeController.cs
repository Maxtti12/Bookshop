
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private BookDbContext db = new BookDbContext();

        public ActionResult Index(string query = "")
        {
            var books = db.Books.Include(b => b.Category);

            if (!string.IsNullOrEmpty(query))
            {
                int categoryId;
                if (int.TryParse(query, out categoryId))
                {
                    // Filter by both title and category ID
                    books = books.Where(b => b.Title.Contains(query) || b.CategoryId == categoryId);
                }
                else
                {
                    // Filter only by title if the query couldn't be parsed as an integer
                    books = books.Where(b => b.Title.Contains(query));
                }
            }

            var categories = db.Categories.ToList();

            ViewBag.BookList = books.ToList();
            ViewBag.CategoryList = categories;

            return View(books.ToList());
        }




        public ActionResult About()
        {
            var books = db.Books;
            ViewBag.BookList = books.ToList();
            return View(books.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}