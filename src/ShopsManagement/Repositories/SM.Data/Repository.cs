using Microsoft.EntityFrameworkCore;
using SM.Data.Interfaces;
using SM.Data.Models;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace SM.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly StoreManagementDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(StoreManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetIncludedEntitiesAsync(Expression<Func<TEntity, bool>> ?filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ?orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, string include)
        {
            return _dbSet.Where(predicate).Include(include);
        }
        public void Delete(TEntity entity)
        {
            if(entity == null)
                return;
            var dbEntity = _context.Entry(entity);

            if(dbEntity.State != EntityState.Deleted)
            {
                dbEntity.State = EntityState.Deleted;
            } 
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
            _context.SaveChanges();
        }

        public void Save(TEntity entity)
        {
            if(entity.Id > 0)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Add(entity);
            }
            _context.SaveChanges();
        }
    }
}
