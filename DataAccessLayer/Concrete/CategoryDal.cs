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
    public class CategoryDal : GenericRepositoryDal<Category,ApplicationDbContext>, ICategoryDal
    {
        public CategoryDal(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
