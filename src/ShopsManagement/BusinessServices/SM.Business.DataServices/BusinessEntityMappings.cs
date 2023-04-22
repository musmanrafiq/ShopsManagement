using AutoMapper;
using SM.Business.Models;
using SM.Data.Models;

namespace SM.Business.DataServices
{
    public class BusinessEntityMappings : Profile
    {
        public BusinessEntityMappings()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<StoreModel, Store>().ReverseMap();
            CreateMap<ArtifactModel, Artifact>().ReverseMap();
        }
    }
}
