using IMS.Api.DTOs.Product;

//Now define the operations your application supports.
namespace IMS.Api.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ListProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(
            CreateProductDto dto,
            int createdBy);

        Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto dto,
            int updatedBy);

        Task<ProductDto?> PatchAsync(
            int id,
            PatchProductDto dto,
            int updatedBy);

        Task<bool> DeleteAsync(
            int id,
            int deletedBy);
    }
}