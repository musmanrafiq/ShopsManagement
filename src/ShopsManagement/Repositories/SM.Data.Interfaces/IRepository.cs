using SM.Data.Models;
using System.Linq.Expressions;

namespace SM.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetIncludedEntitiesAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, string include);
        void Save(TEntity entity);
        void Delete(TEntity entity);

    }
}