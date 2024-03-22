using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
