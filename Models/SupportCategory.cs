namespace YardimMasasi.Models
{
    public class SupportCategory
    {
        public int SupportCategoryId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}