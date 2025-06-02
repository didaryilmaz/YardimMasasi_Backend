using System.ComponentModel.DataAnnotations.Schema;

namespace YardimMasasi.Models
{
    public class SupportCategory
    {
        public int SupportCategoryId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }

}