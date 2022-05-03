using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name ="Name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "The name must contain only letters")]
        public string? CategoryName { get; set; }
        [Required]
        [Display(Name ="Display Order")]
        [Range(1,100)]
        public int CategoryDisplayOrder { get; set; }
        [Display(Name="Date")]
        public DateTime CategoryCreatedDate { get; set; } = DateTime.Now;
    }
}
