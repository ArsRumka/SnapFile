namespace SnapFile.Application.DTOs.RequestDTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int CreatedByUserId { get; set; }
        public string RecipientType { get; set; }
        public int? RecipientUserId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPosition { get; set; }
        public int? RecipientDepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int? FormulationId { get; set; }
    }
}
