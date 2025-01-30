using AutoMapper;
using InvoiceManagement.Models;
using InvoiceManagement.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoice_Management.Models;

namespace InvoiceManagement.Services
{
    public class InvoiceService : IInvoice
    {
        private readonly InvoiceManagementDbContext _context;

        public InvoiceService(InvoiceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
        {
            return await _context.Invoices.Include(i => i.Items).ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            invoice.CreatedAt = DateTime.UtcNow;
            invoice.UpdatedAt = DateTime.UtcNow;
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            invoice.UpdatedAt = DateTime.UtcNow;
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAuditTrailAsync(InvoiceAuditTrail auditTrail)
        {
            _context.InvoiceAuditTrails.Add(auditTrail);
            await _context.SaveChangesAsync();
        }
    }
}
