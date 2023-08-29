using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private BookDbContext db = new BookDbContext();

        // GET: Book
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category).OrderByDescending(p => p.CreatedDate);
            ViewBag.BookList = books.ToList();
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryList = db.Categories.ToList();
            return View(book);
        }


        [Authorize(Roles = "Admin, Staff")]
        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create([Bind(Include = "BookId,Title,Description,Author,Type,Price,Image,CategoryId")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                book.CreatedDate = DateTime.Now;

                // Handle file upload
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    ImageFile.SaveAs(filePath);
                    book.Image = "/Images/" + fileName; 
                }

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Populate a SelectList with categories to display in a dropdown list
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit([Bind(Include = "BookId,Title,Description,Author,Price,CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                var originalBook = db.Books.Find(book.BookId);

                if (originalBook != null)
                {
                    book.Image = originalBook.Image;
                    originalBook.CreatedDate = book.CreatedDate;

                    db.Entry(originalBook).CurrentValues.SetValues(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // If the model state is not valid, repopulate the category dropdown list
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }



        // GET: Book/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
