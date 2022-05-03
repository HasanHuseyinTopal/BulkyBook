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
        ICategoryDal categoryDal { get; }
        ICoverDal coverDal { get; }
        IProductDal productDal { get; }
        void save();
    }
}
