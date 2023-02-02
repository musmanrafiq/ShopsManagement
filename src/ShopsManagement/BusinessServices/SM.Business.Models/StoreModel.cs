namespace SM.Business.Models
{
    public  class StoreModel
    {
        public StoreModel()
        {
            Products = new List<ProductModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<ProductModel> Products { get; set; }
    }
}
