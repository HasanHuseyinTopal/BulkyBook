using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class OrderDetailDal : GenericRepositoryDal<OrderDetail, ApplicationDbContext>, IOrderDetailDal
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailDal(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }

        public void AddRange(List<OrderDetail> orderDetails)
        {
            _context.AddRange(orderDetails);
        }
    }
}
