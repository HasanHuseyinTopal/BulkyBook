using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderHeaderDal : IGenericRepositoryDal<OrderHeader>
    {
        void UpdateStatus(int id, string OrderStatus, string? PaymentStatus=null);
        void UpdateStripePaymentID(int id, string sessionID, string? PaymentItentID = null);

    }
}
