using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.RequestTypeDTOs
{
    public class RequestTypeUpdateDto
    {
        [Required(ErrorMessage = "ID обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название типа заявления обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        public string Name { get; set; }
    }
}
