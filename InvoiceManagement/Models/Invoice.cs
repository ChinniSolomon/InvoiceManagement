namespace InvoiceManagement.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue
    }
}
