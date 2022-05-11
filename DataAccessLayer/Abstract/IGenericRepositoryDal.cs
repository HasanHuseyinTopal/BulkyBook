using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepositoryDal<TEntity> where TEntity : class,new()
    {
        void Add(TEntity Entity);
        void Delete(TEntity Entity);
        void Update(TEntity Entity);
        void DeleteRange(IEnumerable<TEntity> EntityList);
        TEntity GetByID(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null);
       
    }
}
