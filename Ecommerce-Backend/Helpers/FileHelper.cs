using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecommerce_Backend.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile file, string wwwRootPath)
        {
            if (file == null || file.Length == 0) return null;

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var folder = Path.Combine(wwwRootPath, "uploads", "products");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // return relative URL (e.g. /uploads/products/{fileName})
            return $"/uploads/products/{fileName}";
        }

        public static void DeleteImageIfExists(string wwwRootPath, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            // imageUrl expected like "/uploads/products/xxx.jpg"
            var path = imageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
            var full = Path.Combine(wwwRootPath, path);
            if (File.Exists(full)) File.Delete(full);
        }
    }
}
