namespace SnapFile.Domain.Entities
{
    public class TemplateVariable
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

        public string Name { get; set; }   // date, reason
        public string Type { get; set; }   // string, date, number
        public bool IsRequired { get; set; }
    }
}
