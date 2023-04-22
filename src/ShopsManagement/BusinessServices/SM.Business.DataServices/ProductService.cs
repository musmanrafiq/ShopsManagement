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

        public List<ProductModel> ProductsForStore(int storeId, string? searchTerm)
        {
            var productsQurable = _repositry.Get(x => x.StoreId == storeId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                productsQurable = productsQurable.Where(x => x.Name.ToLower()
                    .Contains(searchTerm) || x.Description.ToLower()
                    .Contains(searchTerm) || x.Location.ToLower()
                    .Contains(searchTerm));
            }
            var productModels = productsQurable.Select(x => new ProductModel
            { Id = x.Id, Name = x.Name, Description = x.Description, 
            Location = x.Location,
                StoreId = x.StoreId,
            Artifacts = x.Artifacts.Select(y=> new ArtifactModel { Id = y.Id, Path = y.Path, Name = y.Name }).ToList()}).ToList();
            return productModels;
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