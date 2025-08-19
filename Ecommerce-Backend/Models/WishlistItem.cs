namespace Ecommerce_Backend.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }       // FK to User
        public int ProductId { get; set; }    // FK to Product

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
