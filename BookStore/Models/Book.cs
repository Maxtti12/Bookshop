using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        [Required]
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public string Image { get; set; }

        [DefaultValue(0)]
        public int Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
