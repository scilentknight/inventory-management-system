using IMS.Api.DTOs.Category;

//Now define the operations your application supports.
namespace IMS.Api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<ListCategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task<CategoryDto> CreateAsync(
            CategoryCreateDto dto,
            int createdBy);

        Task<CategoryDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto,
            int updatedBy);

        Task<bool> DeleteAsync(
            int id,
            int deletedBy);
    }
}