using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ProductDetailsModel
    {
        public Product product { get; set; }
        public int Count { get; set; }
    }
}
