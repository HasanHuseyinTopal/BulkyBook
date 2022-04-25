using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepositoryDal<TEntity,TContext> : IGenericRepositoryDal<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext context;
        public GenericRepositoryDal(TContext _context)
        {
            context = _context;
        }
        public void Add(TEntity Entity)
        {
                context.Entry(Entity).State = EntityState.Added;
                context.SaveChanges();
        }
        public void Update(TEntity Entity)
        {
                context.Entry(Entity).State = EntityState.Modified;
                context.SaveChanges();
        }
        public void Delete(TEntity Entity)
        {
                context.Entry(Entity).State = EntityState.Deleted;
                context.SaveChanges();
        }
        public TEntity GetByID(Expression<Func<TEntity, bool>> filter)
        {
                return context.Set<TEntity>().FirstOrDefault(filter);
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter=null)
        {
                if (filter==null)
                {
                return context.Set<TEntity>().AsQueryable();
                }
                else
                {
                return context.Set<TEntity>().Where(filter).AsQueryable();
                }
        }

    }
}
