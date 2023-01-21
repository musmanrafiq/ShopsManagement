using SM.Data.Interfaces;
using SM.Data.Models;


namespace SM.Data
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(StoreManagementDbContext context): base(context)
        {

        }
    }
}
