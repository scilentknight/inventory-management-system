using IMS.Api.Models;

//The repository defines what database operations are available.
namespace IMS.Api.Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetBySkuAsync(string sku);

        Task AddAsync(Product product);

        void Update(Product product);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}