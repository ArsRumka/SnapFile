using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.FormulationDTOs
{
    public class FormulationUpdateDto
    {
        [Required(ErrorMessage = "ID обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Текст формулировки обязателен")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Текст должен быть от 1 до 1000 символов")]
        public string Text { get; set; }
    }
}
