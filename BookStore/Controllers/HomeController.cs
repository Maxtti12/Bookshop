
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

        public ActionResult Index()
        {
            var books = db.Books;
            ViewBag.BookList = books.ToList();
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