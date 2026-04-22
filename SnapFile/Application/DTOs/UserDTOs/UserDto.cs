namespace SnapFile.Application.DTOs.UserDTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? PositionId { get; set; }
        public string PositionName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
    }
}
