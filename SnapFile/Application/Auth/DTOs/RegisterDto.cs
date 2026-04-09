namespace SnapFile.Application.Auth.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
