using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUnitOfWork
    {
        ICompanyDal companyDal { get; }
        ICategoryDal categoryDal { get; }
        ICoverDal coverDal { get; }
        IProductDal productDal { get; }
        IShoppingCartDal ShoppingCartDal { get; }
        IApplicationUserDal ApplicationUserDal { get; }
        IOrderHeaderDal OrderHeaderDal { get; }
        IOrderDetailDal OrderDetailDal { get; }
        void save();
    }
}
