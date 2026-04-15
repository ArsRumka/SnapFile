namespace SnapFile.Application.DTOs.RequestValueDTOs
{
    public class RequestValueDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int TemplateVariableId { get; set; }
        public string Value { get; set; }
    }
}
