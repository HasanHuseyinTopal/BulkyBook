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
    public class ApplicationUserDal : GenericRepositoryDal<ApplicationUser,ApplicationDbContext>, IApplicationUserDal
    {
        public ApplicationUserDal(ApplicationDbContext _context) : base(_context)
        {
        }

    }
}
