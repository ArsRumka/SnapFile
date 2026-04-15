using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.RequestValueDTOs
{
    public class RequestValueCreateDto
    {
        [Required(ErrorMessage = "ID заявления обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID заявления должен быть положительным числом")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "ID переменной шаблона обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID переменной должен быть положительным числом")]
        public int TemplateVariableId { get; set; }

        [Required(ErrorMessage = "Значение обязательно")]
        [StringLength(5000, MinimumLength = 0, ErrorMessage = "Значение не должно превышать 5000 символов")]
        public string Value { get; set; } = string.Empty;
    }
}
