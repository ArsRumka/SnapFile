namespace SnapFile.Domain.Entities
{
    public class EmailCode
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Code { get; set; }

        public DateTime ExpireAt { get; set; }

        public bool IsUsed { get; set; }

        public int Attempts { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
