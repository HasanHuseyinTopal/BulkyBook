using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name ="Description")]
        public string ProductDescription { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name ="Category Name")]
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category category { get; set; }
        [Required]
        [Display(Name = "Cover Name")]
        public int CoverID { get; set; }
        [ValidateNever]
        public Cover cover { get; set; }
    }
}
