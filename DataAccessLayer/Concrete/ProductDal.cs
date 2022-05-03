using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ProductDal : GenericRepositoryDal<Product, ApplicationDbContext>, IProductDal
    {
        private readonly ApplicationDbContext _context;
        public ProductDal(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }
        public IEnumerable<Product> GetAllWithProductAndCover()
        {
            return _context.products.Include(x => x.category).Include(x => x.cover);

        }
    }
}
