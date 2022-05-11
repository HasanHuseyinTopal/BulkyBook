using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class OrderHeaderDal : GenericRepositoryDal<OrderHeader, ApplicationDbContext>, IOrderHeaderDal
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateStatus(int id, string OrderStatus, string? PaymentStatus = null)
        {
            var OrderFromDB = _context.orderHeaders.FirstOrDefault(x => x.OrderHeaderID == id);
            if (OrderFromDB != null)
            {
                OrderFromDB.OrderStatus = OrderStatus;
                if (PaymentStatus != null)
                {
                    OrderFromDB.PaymentStatus = PaymentStatus;
                }
            }
        }
        public void UpdateStripePaymentID(int id, string sessionID, string? PaymentItentID = null)
        {
            var OrderFromDB = _context.orderHeaders.FirstOrDefault(x => x.OrderHeaderID == id);
            OrderFromDB.SessionID = sessionID;
            OrderFromDB.PaymentIntendID = PaymentItentID;
        }
    }
}
