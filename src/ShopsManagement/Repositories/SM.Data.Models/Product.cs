using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Data.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Store = new Store();
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}