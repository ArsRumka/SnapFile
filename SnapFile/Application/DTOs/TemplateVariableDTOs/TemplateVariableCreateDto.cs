using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.TemplateVariableDTOs
{
    public class TemplateVariableCreateDto
    {
        [Required(ErrorMessage = "ID шаблона обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID шаблона должен быть положительным числом")]
        public int TemplateId { get; set; }

        [Required(ErrorMessage = "Название переменной обязательно")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Тип переменной обязателен")]
        [RegularExpression(@"^(string|text|date|number|email|phone|checkbox|radio|select)$", 
            ErrorMessage = "Недопустимый тип переменной")]
        public string Type { get; set; }

        public bool IsRequired { get; set; } = false;
    }
}
