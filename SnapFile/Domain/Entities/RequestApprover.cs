namespace SnapFile.Domain.Entities
{
    public class RequestApprover
    {
        public int Id { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Order { get; set; }

        public DateTime? DecisionDate { get; set; }

    }
}
