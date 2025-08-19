namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminUserAddressDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string AddressLine { get; set; } = "";
        public string Pincode { get; set; } = "";
    }
}
