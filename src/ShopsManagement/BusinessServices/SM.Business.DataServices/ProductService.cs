using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data;
using SM.Data.Interfaces;
using SM.Data.Models;

namespace SM.Business.DataServices
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _dbContext;

        public ProductService(IRepository<Product> dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductModel> GetAll()
        {
            var allProducts = _dbContext.GetAll();
            var productModels = allProducts.Select(x => new ProductModel 
            { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
            return productModels;
        }

        public List<ProductModel> Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim().ToLower();
            var allProducts = _dbContext.Get(x => x.Name.ToLower()
                .Contains(searchTerm) || x.Description.ToLower()
                .Contains(searchTerm)).ToList();

            var productModels = allProducts.Select(x => new ProductModel
            { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
            return productModels;
        }

        public void Add(ProductModel model)
        {
            _dbContext.Save(new Data.Models.Product {  Id = model.Id, Name = model.Name, Description = model.Description});
            //_dbContext.SaveChanges();
        }
        public void Update(ProductModel model)
        {
            _dbContext.Save(new Product { Id = model.Id, Name = model.Name, Description = model.Description });
        }

        public void Delete(int id)
        {
            var productToDelete = _dbContext.Get(x => x.Id == id).FirstOrDefault();
            if (productToDelete != null)
            {
                _dbContext.Delete(productToDelete);
            }
        }
    }
}