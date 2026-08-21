using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Repositories.Products
{
    // This is where your actual Entity Framework Core database operations happen.
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x =>
                    x.Sku == sku &&
                    !x.IsDeleted);
        }

        public async Task<Product?> GetByProductCodeAsync(
            string productCode)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x =>
                    x.ProductCode == productCode &&
                    !x.IsDeleted);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products
                .AnyAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}