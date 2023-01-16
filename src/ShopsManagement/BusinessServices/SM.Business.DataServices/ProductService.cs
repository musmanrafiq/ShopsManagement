using SM.Business.Interfaces;
using SM.Business.Models;

namespace SM.Business.DataServices
{
    public class ProductServicev : IProductService
    {
        private List<ProductModel> products = new List<ProductModel>();

        public List<ProductModel> GetAll()
        {
            products.Add(new ProductModel { Id = 1, Name = "Product 1" });
            products.Add(new ProductModel { Id = 2, Name = "Product 2" });
            products.Add(new ProductModel { Id = 3, Name = "Product 3" });
            products.Add(new ProductModel { Id = 4, Name = "Product 4" });
            products.Add(new ProductModel { Id = 5, Name = "Product 5" });

            return products;
        }
        
        public void Add(ProductModel model)
        {
            products.Add(model);
        }

        public void Delete(int id)
        {
            var productToDelete = products.Where(x => x.Id == id).FirstOrDefault();
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
        }
    }
}