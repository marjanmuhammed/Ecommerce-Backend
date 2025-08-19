using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories.Admin
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminUserRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<UserDetailDTO>> GetAllUsersAsync()
        {
            return await _db.users
                .Select(u => new UserDetailDTO
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    EmailAddress = u.EmailAddress,
                    IsBlocked = u.IsBlocked,
                    Role = u.Role
                })
                .ToListAsync();
        }

        public async Task<UserDetailDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _db.users
                .Where(u => u.Id == userId)
                .Select(u => new UserDetailDTO
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    EmailAddress = u.EmailAddress,
                    IsBlocked = u.IsBlocked,
                    Role = u.Role,

                    Addresses = u.Addresses.Select(a => new AdminUserAddressDTO
                    {
                        Id = a.Id,
                        FullName = a.FullName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        AddressLine = a.AddressLine,
                        Pincode = a.Pincode
                    }),

                    CartItems = u.CartItems.Select(ci => new AdminUserCartItemDTO
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Quantity = ci.Quantity,
                        UnitPrice = ci.Product.Price
                    }),

                    Orders = u.Orders.Select(o => new AdminUserOrderDTO
                    {
                        Id = o.Id,
                        CreatedAt = o.OrderDate,
                        Status = o.Status,
                        Total = o.OrderItems.Sum(oi => oi.Quantity * oi.Price),
                        Items = o.OrderItems.Select(oi => new AdminUserOrderItemDTO
                        {
                            ProductId = oi.ProductId,
                            ProductName = oi.Product.Name,
                            ProductImageUrl = oi.Product.ImageUrl,  
                            Quantity = oi.Quantity,
                            UnitPrice = oi.Price
                        })
                    })
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> BlockUserAsync(int userId)
        {
            var user = await _db.users.FindAsync(userId);
            if (user == null) return false;

            user.IsBlocked = true;
            _db.users.Update(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockUserAsync(int userId)
        {
            var user = await _db.users.FindAsync(userId);
            if (user == null) return false;

            user.IsBlocked = false;
            _db.users.Update(user);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _db.users.FindAsync(id);
            if (user == null) return false;

            _db.users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
