public class AdminUserOrderItemDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";

    public string ProductImageUrl { get; set; } = "";  // <-- Image URL added
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
