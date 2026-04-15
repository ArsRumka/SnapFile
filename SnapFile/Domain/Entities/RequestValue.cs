namespace SnapFile.Domain.Entities
{
    public class RequestValue
    {
        public int Id { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int TemplateVariableId { get; set; }
        public TemplateVariable TemplateVariable { get; set; }

        public string Value { get; set; }
    }
}
