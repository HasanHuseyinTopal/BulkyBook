using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [DisplayName("Name")]
        public string? CategoryName { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1,100)]
        public int CategoryDisplayOrder { get; set; }
        [DisplayName("Date")]
        public DateTime CategoryCreatedDate { get; set; } = DateTime.Now;
    }
}
