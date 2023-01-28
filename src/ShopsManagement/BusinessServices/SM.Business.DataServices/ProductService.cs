using AutoMapper;
using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data;
using SM.Data.Interfaces;
using SM.Data.Models;

namespace SM.Business.DataServices
{
    public class ProductService : GenericService<ProductModel, Product>, IProductService
    {
        private readonly IRepository<Product> _repositry;

        public ProductService(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
            _repositry = repository;
        }

        public List<ProductModel> Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim().ToLower();
            var allProducts = _repositry.Get(x => x.Name.ToLower()
                .Contains(searchTerm) || x.Description.ToLower()
                .Contains(searchTerm)).ToList();

            var productModels = allProducts.Select(x => new ProductModel
            { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
            return productModels;
        }

    }
}