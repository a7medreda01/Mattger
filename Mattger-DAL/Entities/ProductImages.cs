using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mattger_DAL.Entities
{
    public class ProductImages
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = default!;

        // 🔗 Foreign Key
        public int ProductId { get; set; }

        // 🔗 Navigation Property
        public Product Product { get; set; } = default!;
    }
}