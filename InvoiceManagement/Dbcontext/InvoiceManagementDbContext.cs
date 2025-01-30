using InvoiceManagement.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Management.Models
{
    public class InvoiceManagementDbContext : DbContext
    {
        public InvoiceManagementDbContext(DbContextOptions<InvoiceManagementDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<InvoiceAuditTrail> InvoiceAuditTrails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Items)
                .WithOne(i => i.Invoice)
                .HasForeignKey(i => i.InvoiceId);

            modelBuilder.Entity<AuditLog>()
                .HasOne<Invoice>()
                .WithMany()
                .HasForeignKey(a => a.InvoiceId);
        }
    }
}
