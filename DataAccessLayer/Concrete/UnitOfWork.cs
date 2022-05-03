using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryDal categoryDal { get; }
        public ICoverDal coverDal { get; }
        public IProductDal productDal { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context=context;
            categoryDal = new CategoryDal(_context);
            coverDal = new CoverDal(_context);
            productDal = new ProductDal(_context);
        }
        public void save()
        {
            _context.SaveChanges();
        }
    }
}
