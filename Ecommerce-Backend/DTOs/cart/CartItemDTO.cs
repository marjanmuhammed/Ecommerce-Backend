namespace Ecommerce_Backend.DTOs.Cart


    
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
