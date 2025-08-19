using Ecommerce_Backend.DTOs;

public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public AddressDTO Address { get; set; }
    public List<OrderItemResponseDto> Items { get; set; }

    public UserDto User { get; set; }  // if you want to show user info
}
