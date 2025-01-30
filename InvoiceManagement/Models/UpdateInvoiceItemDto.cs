namespace InvoiceManagement.Models
{
    public class UpdateInvoiceItemDto
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
