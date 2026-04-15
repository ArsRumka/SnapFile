namespace SnapFile.Domain.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        // 👇 Получатель
        public string RecipientType { get; set; } // "Employee" / "Custom"

        public int? RecipientUserId { get; set; }
        public User RecipientUser { get; set; }

        public string? RecipientName { get; set; }
        public string? RecipientPosition { get; set; }

        public int? RecipientDepartmentId { get; set; }
        public Department RecipientDepartment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Draft";

        public int? FormulationId { get; set; }
        public Formulation Formulation { get; set; }

        public ICollection<RequestValue> Values { get; set; } = new List<RequestValue>();
        public ICollection<RequestApprover> Approvers { get; set; } = new List<RequestApprover>();
    }
}
