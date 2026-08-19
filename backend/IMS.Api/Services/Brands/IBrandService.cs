using IMS.Api.DTOs.Brand;

// Now define the operations your application supports.
namespace IMS.Api.Services.Brands
{
    public interface IBrandService
    {
        Task<IEnumerable<ListBrandDto>> GetAllAsync();

        Task<BrandDto?> GetByIdAsync(int id);

        Task<BrandDto> CreateAsync(
            CreateBrandDto dto,
            int createdBy);

        Task<BrandDto?> UpdateAsync(
            int id,
            UpdateBrandDto dto,
            int updatedBy);

        Task<bool> DeleteAsync(
            int id,
            int deletedBy);
    }
}