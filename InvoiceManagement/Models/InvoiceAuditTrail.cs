namespace InvoiceManagement.Models
{
    public class InvoiceAuditTrail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ActionType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
