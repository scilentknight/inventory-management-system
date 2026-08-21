using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.User
{
    public class AssignRoleDto
    {
        [Required]
        public int RoleId { get; set; }
    }
}