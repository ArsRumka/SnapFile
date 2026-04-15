namespace SnapFile.Application.DTOs.TemplateVariableDTOs
{
    public class TemplateVariableDto
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
