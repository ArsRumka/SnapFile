using System.ComponentModel.DataAnnotations;

namespace SnapFile.Application.DTOs.RequestApproverDTOs
{
    public class RequestApproverUpdateDto
    {
        [Required(ErrorMessage = "ID обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ID заявления обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID заявления должен быть положительным числом")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "ID пользователя обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID пользователя должен быть положительным числом")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Порядок согласования обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Порядок должен быть положительным числом")]
        public int Order { get; set; }

        public DateTime? DecisionDate { get; set; }
    }
}
