using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.TemplateDTOs
{
    public class TemplateCreateDto
    {
        [Required(ErrorMessage = "Название шаблона обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ID типа заявления обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID типа заявления должен быть положительным числом")]
        public int RequestTypeId { get; set; }

        [Required(ErrorMessage = "HTML содержимое обязательно")]
        [MinLength(1, ErrorMessage = "HTML содержимое не может быть пустым")]
        public string HtmlContent { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ID формулировки должен быть положительным числом")]
        public int? FormulationId { get; set; }
    }
}
