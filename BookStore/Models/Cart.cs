using System.Collections.Generic;

namespace BookStore.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void AddToCart(Book book)
        {
            // Check if the book is already in the cart
            var existingCartItem = CartItems.Find(item => item.Book.BookId == book.BookId);

            if (existingCartItem != null)
            {
                // If the book is already in the cart, increment the quantity
                existingCartItem.Quantity++;
            }
            else
            {
                // If the book is not in the cart, add it with quantity 1
                CartItems.Add(new CartItem { Book = book, Quantity = 1 });
            }
        }

        public void RemoveFromCart(Book book)
        {
            var existingCartItem = CartItems.Find(item => item.Book.BookId == book.BookId);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity > 1)
                {
                    // If the quantity is greater than 1, decrement it
                    existingCartItem.Quantity--;
                }
                else
                {
                    // If the quantity is 1 or less, remove the book from the cart
                    CartItems.Remove(existingCartItem);
                }
            }
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }

    }

    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
