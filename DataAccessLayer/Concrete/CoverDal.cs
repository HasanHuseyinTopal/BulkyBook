using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class CoverDal : GenericRepositoryDal<Cover,ApplicationDbContext>,ICoverDal
    {
        public CoverDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
