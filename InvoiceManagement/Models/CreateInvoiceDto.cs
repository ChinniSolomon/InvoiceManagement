namespace InvoiceManagement.Models
{
    public class CreateInvoiceDto
    {
        public InvoiceStatus Status { get; set; }
        public IEnumerable<CreateInvoiceItemDto> Items { get; set; }
    }
}
