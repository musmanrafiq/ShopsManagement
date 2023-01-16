using SM.Business.Models;

namespace SM.Business.Interfaces
{
    public interface IProductService
    {
        public List<ProductModel> GetAll();
        public void Add(ProductModel model);
        public void Delete(int id);
    }
}
