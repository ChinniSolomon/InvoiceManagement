namespace InvoiceManagement.Models
{
    public class UpdateInvoiceDto
    {
        public InvoiceStatus? Status { get; set; }
        public IEnumerable<UpdateInvoiceItemDto> Items { get; set; } 
    }
}
