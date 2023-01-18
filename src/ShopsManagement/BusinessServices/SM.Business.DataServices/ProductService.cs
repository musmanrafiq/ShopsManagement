using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data;

namespace SM.Business.DataServices
{
    public class ProductService : IProductService
    {
        private readonly StoreManagementDbContext _dbContext;

        public ProductService(StoreManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductModel> GetAll()
        {
            var allProducts = _dbContext.Products.ToList();
            var productModels = allProducts.Select(x => new ProductModel 
            { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
            return productModels;
        }

        public List<ProductModel> Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim().ToLower();
            var allProducts = _dbContext.Products.Where(x => x.Name.ToLower()
                .Contains(searchTerm) || x.Description.ToLower()
                .Contains(searchTerm)).ToList();

            var productModels = allProducts.Select(x => new ProductModel
            { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
            return productModels;
        }

        public void Add(ProductModel model)
        {
            _dbContext.Products.Add(new Data.Models.Product {  Id = model.Id, Name = model.Name, Description = model.Description});
            _dbContext.SaveChanges();
        }
        public void Update(ProductModel model)
        {
            var entity = _dbContext.Products.FirstOrDefault(x => x.Id == model.Id);
            if(entity != null)
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var productToDelete = _dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}