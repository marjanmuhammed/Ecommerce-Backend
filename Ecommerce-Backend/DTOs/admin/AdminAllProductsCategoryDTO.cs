namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminAllProductsCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";


        // Add images as list of strings (URLs)
        public string ImageUrl { get; set; } = "";  // single image url
    }
}
