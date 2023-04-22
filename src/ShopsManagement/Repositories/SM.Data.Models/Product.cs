using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Data.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Store = new Store();
            Artifacts = new List<Artifact>();
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // to save a products location insie a store
        public string Location { get; set; }

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public ICollection<Artifact> Artifacts { get; set; }
    }
}