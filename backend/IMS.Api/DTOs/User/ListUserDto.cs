//namespace IMS.Api.DTOs.User
//{
//    public class ListUserDto
//    {
//        public int Id { get; set; }

//        public string Email { get; set; } = string.Empty;

//        public string Role { get; set; } = string.Empty;

//        public bool IsActive { get; set; }

//        public DateTime CreatedAt { get; set; }
//    }
//}

namespace IMS.Api.DTOs.User
{
    public class ListUserDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}