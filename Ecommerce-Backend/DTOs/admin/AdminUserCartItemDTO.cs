namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminUserCartItemDTO
    {
        public int Id { get; set; }       // cart item id
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
