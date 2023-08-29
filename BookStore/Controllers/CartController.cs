using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using Microsoft.AspNet.Identity;

public class CartController : Controller
{
    private BookDbContext db = new BookDbContext();

    public ActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    public ActionResult AddToCart(int? bookId)
    {
        if (bookId == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var book = db.Books.Find(bookId);
        if (book == null)
        {
            return HttpNotFound();
        }

        var cart = GetCart();
        cart.AddToCart(book);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    public ActionResult RemoveFromCart(int? bookId)
    {
        if (bookId == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var book = db.Books.Find(bookId);
        if (book == null)
        {
            return HttpNotFound();
        }

        var cart = GetCart();
        cart.RemoveFromCart(book);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    private Cart GetCart()
    {
        var cart = Session["Cart"] as Cart;
        if (cart == null)
        {
            cart = new Cart();
            Session["Cart"] = cart;
        }
        return cart;
    }

    private void SaveCart(Cart cart)
    {
        Session["Cart"] = cart;
    }

    public ActionResult Checkout()
    {
        var cart = GetCart();

        if (cart.CartItems.Count == 0)
        {
            ViewBag.Message = "Your cart is empty.";
            return View("EmptyCart");
        }
        var order = new Order
        {
            CustomerId = User.Identity.GetUserId(),
            CreatedDate = DateTime.Now,
            OrderDetails = cart.CartItems.Select(item => new OrderDetail
            {
                BookId = item.Book.BookId,
                Quantity = item.Quantity
            }).ToList()
        };

        db.Orders.Add(order);
        db.SaveChanges();

        cart.ClearCart();
        SaveCart(cart);

        return RedirectToAction("Details", "Order", new { id = order.OrderId });
    }

}
