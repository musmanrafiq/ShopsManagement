using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Data.Models
{
    public class Artifact: BaseEntity
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
