using SM.Business.Models;

namespace SM.Business.Interfaces
{
    public interface IStoreService : IGenericService<StoreModel>
    {
        public string GetStoreNameById(int storeId);
    }
}
