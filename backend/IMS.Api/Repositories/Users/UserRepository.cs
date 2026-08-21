//using IMS.Api.Data;
//using IMS.Api.Models;
//using Microsoft.EntityFrameworkCore;

//namespace IMS.Api.Repositories.Users
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public UserRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<User>> GetAllAsync()
//        {
//            return await _context.Users
//                .AsNoTracking()
//                .OrderBy(u => u.Id)
//                .ToListAsync();
//        }

//        public async Task<User?> GetByIdAsync(int id)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(u => u.Id == id);
//        }

//        public async Task<User?> GetByEmailAsync(string email)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(u => u.Email == email);
//        }

//        public async Task AddAsync(User user)
//        {
//            await _context.Users.AddAsync(user);
//        }

//        public void Update(User user)
//        {
//            _context.Users.Update(user);
//        }

//        public void Delete(User user)
//        {
//            _context.Users.Remove(user);
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

namespace IMS.Api.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(
            string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}