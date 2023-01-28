using SM.Data.Interfaces;
using SM.Data.Models;

namespace SM.Data
{
    public class StoreRepository : Repository<Store>,IStoreRepository
    {
        public StoreRepository(StoreManagementDbContext context): base(context)
        {
        }
    }
}
