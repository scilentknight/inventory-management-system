using IMS.Api.Models;

namespace IMS.Api.Repositories.Products
{
    // The repository defines what database operations are available.
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetBySkuAsync(string sku);

        Task<Product?> GetByProductCodeAsync(string productCode);

        Task AddAsync(Product product);

        void Update(Product product);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}