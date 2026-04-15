using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.FormulationDTOs
{
    public class FormulationCreateDto
    {
        [Required(ErrorMessage = "Текст формулировки обязателен")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Текст должен быть от 1 до 1000 символов")]
        public string Text { get; set; }
    }
}
