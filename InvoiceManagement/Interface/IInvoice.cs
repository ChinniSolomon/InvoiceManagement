using InvoiceManagement.Models;
using System.Threading.Tasks;

namespace InvoiceManagement.Interface
{
    public interface IInvoice
    {
        Task<IEnumerable<Invoice>> GetInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);

        Task AddAuditTrailAsync(InvoiceAuditTrail auditTrail);
    }
}
