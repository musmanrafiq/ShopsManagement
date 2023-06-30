using System.Linq.Expressions;

namespace SM.Business.Interfaces
{
    public interface IGenericService<TModel>
    {
        public List<TModel> GetAll();
        public Task<List<TModel>> GetIncludedEntitiesAsync(string includeProperties = "");
        public TModel GetById(int id);
        public TModel GetByIdWithInclude(int id, string include);
        public void Add(TModel model);
        public void Update(TModel model);
        public void Delete(int id);
    }
}
