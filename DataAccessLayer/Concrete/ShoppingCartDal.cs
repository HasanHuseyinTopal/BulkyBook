using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ShoppingCartDal : GenericRepositoryDal<ShoppingCart,ApplicationDbContext>, IShoppingCartDal
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ShoppingCart> GetAllWithProduct(Expression<Func<ShoppingCart, bool>> filter)
        {
            return _context.shoppingCarts.Include(x => x.product).Where(filter);
        } 

    }
}
