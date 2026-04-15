namespace SnapFile.Application.DTOs.RequestApproverDTOs
{
    public class RequestApproverDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int Order { get; set; }
        public DateTime? DecisionDate { get; set; }
    }
}
