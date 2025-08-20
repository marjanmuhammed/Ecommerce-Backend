namespace Ecommerce_Backend.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;

        // 👇 Add this
        public int UserId { get; set; }
    }
}
