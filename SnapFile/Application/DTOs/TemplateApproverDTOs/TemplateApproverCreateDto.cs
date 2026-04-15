using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.TemplateApproverDTOs
{
    public class TemplateApproverCreateDto
    {
        [Required(ErrorMessage = "ID шаблона обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID шаблона должен быть положительным числом")]
        public int TemplateId { get; set; }

        [Required(ErrorMessage = "ID пользователя обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID пользователя должен быть положительным числом")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Порядок согласования обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Порядок должен быть положительным числом")]
        public int Order { get; set; }
    }
}
