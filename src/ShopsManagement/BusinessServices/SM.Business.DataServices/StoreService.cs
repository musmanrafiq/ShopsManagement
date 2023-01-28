using AutoMapper;
using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data.Interfaces;
using SM.Data.Models;

namespace SM.Business.DataServices
{
    public class StoreService : GenericService<StoreModel, Store>, IStoreService
    {
        public StoreService(IRepository<Store> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}