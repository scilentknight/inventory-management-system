namespace IMS.Api.DTOs.Unit
{
    // Used for listing units efficiently
    public class ListUnitDto
    {
        public int Id { get; set; }

        public string UnitCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}