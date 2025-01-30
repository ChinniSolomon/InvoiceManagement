namespace InvoiceManagement.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
