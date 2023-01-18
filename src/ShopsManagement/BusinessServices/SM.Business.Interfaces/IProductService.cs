using SM.Business.Models;

namespace SM.Business.Interfaces
{
    public interface IProductService
    {
        public List<ProductModel> GetAll();
        public List<ProductModel> Search(string searchTerm);
        public void Add(ProductModel model);
        public void Update(ProductModel model);
        public void Delete(int id);
    }
}
