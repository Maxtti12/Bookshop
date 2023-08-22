using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Models
{

    public enum Type
    {
        Fiction, Nonfiction, Classics, Tragedy, ScienceFiction, Fantasy, Fairytale, Adventure,
        Satire, Romance, Horror, Dystopian
    }

    public class Book
    {
        [Required]
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Image { get; set; }


        [DefaultValue("None")]
        public Type? Type { get; set; }

        [DefaultValue(0)]
        public int Price { get;set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //public virtual Author Author { get; set; }

    }
}