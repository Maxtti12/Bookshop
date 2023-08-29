using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using Microsoft.AspNet.Identity;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private BookDbContext db = new BookDbContext();

        // GET: Order
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId(); 

            bool isAdmin = User.IsInRole("Admin");

            var userOrders = isAdmin ? db.Orders.ToList() : db.Orders.Where(o => o.CustomerId == userId).ToList();

            return View(userOrders);
        }

        // GET: Order/Details/5
        
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
