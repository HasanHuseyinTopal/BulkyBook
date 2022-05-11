using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Concrete
{
    public class GenericRepositoryDal<TEntity, TContext> : IGenericRepositoryDal<TEntity> where TEntity : class,new() where TContext : DbContext
    {
        private ApplicationDbContext _context;
        private DbSet<TEntity> dbSet;

        public GenericRepositoryDal(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }
        public void Add(TEntity Entity)
        {
        dbSet.Add(Entity);
        }
        public void Update(TEntity Entity)
        {
            dbSet.Update(Entity);
     
        }
        public void Delete(TEntity Entity)
        {
            dbSet.Remove(Entity);
          
        }
        public void DeleteRange(IEnumerable<TEntity> EntityList)
        {
            dbSet.RemoveRange(EntityList);
        }
        public TEntity GetByID(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            return query.FirstOrDefault(filter);
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter == null)
            {
                IQueryable<TEntity> query = dbSet;
                return query.ToList();
            }
            else
            {
                IQueryable<TEntity> query = dbSet;
                return query.Where(filter).ToList();
            }
        }

       
    }
}
