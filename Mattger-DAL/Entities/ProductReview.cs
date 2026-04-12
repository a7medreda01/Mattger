using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mattger_DAL.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }

        // 🔗 Foreign Key للمنتج
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        // ⭐ التقييمات الخمسة
        [Range(1, 5)]
        public int Quality { get; set; }

        [Range(1, 5)]
        public int Design { get; set; }

        [Range(1, 5)]
        public int Usability { get; set; }

        [Range(1, 5)]
        public int Durability { get; set; }

        [Range(1, 5)]
        public int ValueForMoney { get; set; }

        // Optional: اسم المراجع أو user id
        public string? ReviewerName { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}