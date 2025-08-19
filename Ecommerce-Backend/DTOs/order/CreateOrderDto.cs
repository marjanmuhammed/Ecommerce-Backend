using Ecommerce_Backend.DTOs;


public class CreateOrderDto
{
    public int AddressId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }

    public AddressDTO Address { get; set; }  // New address details
}
