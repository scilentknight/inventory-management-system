//using IMS.Api.Data;
//using IMS.Api.Models;
//using Microsoft.EntityFrameworkCore;

//namespace IMS.Api.Repositories.Auths
//{
//    public class AuthRepository : IAuthRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public AuthRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<User?> GetByEmailAsync(string email)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(u => u.Email == email);
//        }

//        public async Task<User?> GetByIdAsync(int id)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(u => u.Id == id);
//        }

//        public async Task AddAsync(User user)
//        {
//            await _context.Users.AddAsync(user);
//        }

//        public async Task SaveChangesAsync()
//        {
//            await _context.SaveChangesAsync();
//        }
//    }
//}

using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Repositories.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}