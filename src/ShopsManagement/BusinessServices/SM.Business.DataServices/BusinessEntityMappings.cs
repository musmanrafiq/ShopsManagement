using AutoMapper;
using SM.Business.Models;
using SM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Business.DataServices
{
    public class BusinessEntityMappings : Profile
    {
        public BusinessEntityMappings()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<StoreModel, Store>().ReverseMap();
        }
    }
}
