namespace SM.Data.Models
{
    public class Store : BaseEntity
    {
        public Store()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }
    }
}
