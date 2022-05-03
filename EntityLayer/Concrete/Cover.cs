using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Cover
    {
        [Key]
        public int CoverID { get; set; }
        [Required]
        [Display(Name ="Cover Name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "The name must contain only letters")]
        public string? CoverName { get; set; }
    }
}
