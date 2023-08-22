using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{

    public enum Delivery
    {
        PostOffice, Express, Normal
    }

    public enum Payment
    {
        Cash, OnlineBanking, EWallet
    }

    public class Order
    {
        [Required]
        [Key]
        public int OrderId { get; set; }


        public string BookId { get; set; }


        public string CustomerId { get; set; }



        [DefaultValue("None")]
        public Delivery? Delivery { get; set; }


        [DefaultValue("None")]
        public Payment? Payment { get; set; }


        [DefaultValue(0)]
        public int OrderPrice { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public virtual Book Book { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        //public virtual ICollection<Instructor> Instructors { get; set; }

    }
}