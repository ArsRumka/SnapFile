namespace SnapFile.Application.DTOs.UserDTOs
{
    public class UpdateUserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public string Phone { get; set; }
    }
}
