using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}