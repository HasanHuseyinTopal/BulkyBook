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

        public ICompanyDal companyDal { get; }

        public IShoppingCartDal ShoppingCartDal { get; }

        public IApplicationUserDal ApplicationUserDal { get; }

        public IOrderHeaderDal OrderHeaderDal { get; }
        public IOrderDetailDal OrderDetailDal { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context=context;
            OrderHeaderDal = new OrderHeaderDal(_context);
            OrderDetailDal = new OrderDetailDal (_context);
            categoryDal = new CategoryDal(_context);
            coverDal = new CoverDal(_context);
            productDal = new ProductDal(_context);
            companyDal = new CompanyDal(_context);
            ShoppingCartDal = new ShoppingCartDal(_context);
            ApplicationUserDal = new ApplicationUserDal(_context);
        }
        public void save()
        {
            _context.SaveChanges();
        }
    }
}
