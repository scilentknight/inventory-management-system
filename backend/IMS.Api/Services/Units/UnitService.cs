using IMS.Api.DTOs.Unit;
using IMS.Api.Models;
using IMS.Api.Repositories.Units;

namespace IMS.Api.Services.Units
{
    // Business logic for units
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repository;

        public UnitService(IUnitRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ListUnitDto>> GetAllAsync()
        {
            var units = await _repository.GetAllAsync();

            return units.Select(x => new ListUnitDto
            {
                Id = x.Id,
                UnitCode = x.UnitCode,
                Name = x.Name,
                ShortName = x.ShortName,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            });
        }

        public async Task<UnitDto?> GetByIdAsync(int id)
        {
            var unit = await _repository.GetByIdAsync(id);

            if (unit == null)
                return null;

            return MapToDto(unit);
        }

        public async Task<UnitDto> CreateAsync(
            CreateUnitDto dto,
            int createdBy)
        {
            var unitCode = dto.UnitCode.Trim();
            var name = dto.Name.Trim();
            var shortName = dto.ShortName.Trim();

            // Check duplicate UnitCode
            var existingCode =
                await _repository.GetByCodeAsync(unitCode);

            if (existingCode != null)
            {
                throw new InvalidOperationException(
                    "A unit with this unit code already exists.");
            }

            // Check duplicate Name
            var existingName =
                await _repository.GetByNameAsync(name);

            if (existingName != null)
            {
                throw new InvalidOperationException(
                    "A unit with this name already exists.");
            }

            // Check duplicate ShortName
            var existingShortName =
                await _repository.GetByShortNameAsync(shortName);

            if (existingShortName != null)
            {
                throw new InvalidOperationException(
                    "A unit with this short name already exists.");
            }

            var unit = new Unit
            {
                UnitCode = unitCode,
                Name = name,
                ShortName = shortName,
                Description = dto.Description?.Trim(),
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _repository.AddAsync(unit);

            await _repository.SaveChangesAsync();

            return MapToDto(unit);
        }

        public async Task<UnitDto?> UpdateAsync(
            int id,
            UpdateUnitDto dto,
            int updatedBy)
        {
            var unit = await _repository.GetByIdAsync(id);

            if (unit == null)
                return null;

            var unitCode = dto.UnitCode.Trim();
            var name = dto.Name.Trim();
            var shortName = dto.ShortName.Trim();

            // Check duplicate code
            var existingCode =
                await _repository.GetByCodeAsync(unitCode);

            if (existingCode != null &&
                existingCode.Id != id)
            {
                throw new InvalidOperationException(
                    "A unit with this unit code already exists.");
            }

            // Check duplicate name
            var existingName =
                await _repository.GetByNameAsync(name);

            if (existingName != null &&
                existingName.Id != id)
            {
                throw new InvalidOperationException(
                    "A unit with this name already exists.");
            }

            // Check duplicate short name
            var existingShortName =
                await _repository.GetByShortNameAsync(shortName);

            if (existingShortName != null &&
                existingShortName.Id != id)
            {
                throw new InvalidOperationException(
                    "A unit with this short name already exists.");
            }

            unit.UnitCode = unitCode;
            unit.Name = name;
            unit.ShortName = shortName;
            unit.Description = dto.Description?.Trim();
            unit.DisplayOrder = dto.DisplayOrder;
            unit.IsActive = dto.IsActive;

            unit.UpdatedAt = DateTime.UtcNow;
            unit.UpdatedBy = updatedBy;

            _repository.Update(unit);

            await _repository.SaveChangesAsync();

            return MapToDto(unit);
        }

        public async Task<UnitDto?> PatchAsync(
            int id,
            PatchUnitDto dto,
            int updatedBy)
        {
            var unit = await _repository.GetByIdAsync(id);

            if (unit == null)
                return null;

            // Unit Code
            if (dto.UnitCode != null)
            {
                var unitCode = dto.UnitCode.Trim();

                var existingCode =
                    await _repository.GetByCodeAsync(unitCode);

                if (existingCode != null &&
                    existingCode.Id != id)
                {
                    throw new InvalidOperationException(
                        "A unit with this unit code already exists.");
                }

                unit.UnitCode = unitCode;
            }

            // Name
            if (dto.Name != null)
            {
                var name = dto.Name.Trim();

                var existingName =
                    await _repository.GetByNameAsync(name);

                if (existingName != null &&
                    existingName.Id != id)
                {
                    throw new InvalidOperationException(
                        "A unit with this name already exists.");
                }

                unit.Name = name;
            }

            // Short Name
            if (dto.ShortName != null)
            {
                var shortName = dto.ShortName.Trim();

                var existingShortName =
                    await _repository.GetByShortNameAsync(shortName);

                if (existingShortName != null &&
                    existingShortName.Id != id)
                {
                    throw new InvalidOperationException(
                        "A unit with this short name already exists.");
                }

                unit.ShortName = shortName;
            }

            // Description
            if (dto.Description != null)
            {
                unit.Description = dto.Description.Trim();
            }

            // Display Order
            if (dto.DisplayOrder.HasValue)
            {
                unit.DisplayOrder = dto.DisplayOrder.Value;
            }

            // Active status
            if (dto.IsActive.HasValue)
            {
                unit.IsActive = dto.IsActive.Value;
            }

            unit.UpdatedAt = DateTime.UtcNow;
            unit.UpdatedBy = updatedBy;

            _repository.Update(unit);

            await _repository.SaveChangesAsync();

            return MapToDto(unit);
        }

        public async Task<bool> DeleteAsync(
            int id,
            int deletedBy)
        {
            var unit = await _repository.GetByIdAsync(id);

            if (unit == null)
                return false;

            // Do not delete a unit that is being used
            var hasProducts =
                await _repository.HasProductsAsync(id);

            if (hasProducts)
            {
                throw new InvalidOperationException(
                    "This unit cannot be deleted because it is being used by one or more products.");
            }

            unit.IsDeleted = true;
            unit.IsActive = false;
            unit.DeletedAt = DateTime.UtcNow;
            unit.DeletedBy = deletedBy;

            _repository.Update(unit);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static UnitDto MapToDto(Unit unit)
        {
            return new UnitDto
            {
                Id = unit.Id,
                UnitCode = unit.UnitCode,
                Name = unit.Name,
                ShortName = unit.ShortName,
                Description = unit.Description,
                DisplayOrder = unit.DisplayOrder,
                IsActive = unit.IsActive,
                CreatedAt = unit.CreatedAt
            };
        }
    }
}