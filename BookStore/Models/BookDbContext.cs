using System;
using System.Linq;
using System.Data.Entity;

namespace BookStore.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext()
            : base("name=BookDbContext")
        {
        }
        public virtual DbSet<Book> Books { get; set; }

        public System.Data.Entity.DbSet<BookStore.Models.Order> Orders { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}