namespace SM.Business.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Store = new StoreModel();
            Artifacts = new List<ArtifactModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // to save a products location insie a store
        public string Location { get; set; }
        public int StoreId { get; set; }
        public StoreModel Store { get; set; }
        public ICollection<ArtifactModel> Artifacts { get; set; }

    }

}