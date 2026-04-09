using System.ComponentModel.DataAnnotations;

namespace SnapFile.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must contain only letters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Middle name must contain only letters")]
        public string MiddleName { get; set; }

        public int? PositionId { get; set; }
        public Position Position { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }= false;

        [Required]
        public string PasswordHash { get; set; }


        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    }
}
