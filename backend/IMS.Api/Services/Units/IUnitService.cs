using IMS.Api.DTOs.Unit;

namespace IMS.Api.Services.Units
{
    public interface IUnitService
    {
        Task<IEnumerable<ListUnitDto>> GetAllAsync();

        Task<UnitDto?> GetByIdAsync(int id);

        Task<UnitDto> CreateAsync(
            CreateUnitDto dto,
            int createdBy);

        Task<UnitDto?> UpdateAsync(
            int id,
            UpdateUnitDto dto,
            int updatedBy);

        Task<UnitDto?> PatchAsync(
            int id,
            PatchUnitDto dto,
            int updatedBy);

        Task<bool> DeleteAsync(
            int id,
            int deletedBy);
    }
}