namespace SnapFile.Application.DTOs.TemplateDTOs
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RequestTypeId { get; set; }
        public string HtmlContent { get; set; }
        public int? FormulationId { get; set; }
    }
}
