namespace SnapFile.Domain.Entities
{
    public class Template
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }

        public string HtmlContent { get; set; }

        public int? FormulationId { get; set; }
        public Formulation Formulation { get; set; }

        public ICollection<TemplateVariable> Variables { get; set; } = new List<TemplateVariable>();
        public ICollection<TemplateApprover> Approvers { get; set; } = new List<TemplateApprover>();
    }
}
