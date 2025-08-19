using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce_Backend.Helpers
{
    public static class CloudinaryExtensions
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            var cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);  // Cloudinary instance
            services.AddScoped<CloudinaryHelper>(); // Helper class

            return services;
        }
    }
}
