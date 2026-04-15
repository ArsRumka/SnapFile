using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.RequestTypeDTOs
{
    public class RequestTypeCreateDto
    {
        [Required(ErrorMessage = "Название типа заявления обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        public string Name { get; set; }
    }
}
