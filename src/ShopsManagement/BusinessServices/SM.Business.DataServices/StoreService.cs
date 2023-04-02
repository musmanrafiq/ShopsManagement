using AutoMapper;
using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data.Interfaces;
using SM.Data.Models;

namespace SM.Business.DataServices
{
    public class StoreService : GenericService<StoreModel, Store>, IStoreService
    {
        private readonly IRepository<Store> _storeRepository;
        public StoreService(IRepository<Store> repository, IMapper mapper) : base(repository, mapper)
        {
            _storeRepository = repository;
        }

        public string GetStoreNameById(int storeId)
        {
            var storeName = _storeRepository.Get(x => x.Id == storeId)
                .Select(x => x.Name).FirstOrDefault();
            return storeName ?? string.Empty;
        }
    }
}