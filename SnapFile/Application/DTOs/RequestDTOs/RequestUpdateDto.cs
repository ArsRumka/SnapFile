using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.RequestDTOs
{
    public class RequestUpdateDto
    {
        [Required(ErrorMessage = "ID обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ID шаблона обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID шаблона должен быть положительным числом")]
        public int TemplateId { get; set; }

        [Required(ErrorMessage = "ID создателя обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID создателя должен быть положительным числом")]
        public int CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Тип получателя обязателен")]
        [RegularExpression(@"^(Employee|Custom)$", ErrorMessage = "Недопустимый тип получателя")]
        public string RecipientType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ID получателя должен быть положительным числом")]
        public int? RecipientUserId { get; set; }

        [StringLength(200, ErrorMessage = "Имя получателя не должно превышать 200 символов")]
        public string RecipientName { get; set; }

        [StringLength(200, ErrorMessage = "Должность не должна превышать 200 символов")]
        public string RecipientPosition { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ID отдела должен быть положительным числом")]
        public int? RecipientDepartmentId { get; set; }

        [Required(ErrorMessage = "Статус обязателен")]
        [RegularExpression(@"^(Draft|Submitted|InApproval|Approved|Rejected|Cancelled)$", 
            ErrorMessage = "Недопустимый статус")]
        public string Status { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ID формулировки должен быть положительным числом")]
        public int? FormulationId { get; set; }
    }
}
