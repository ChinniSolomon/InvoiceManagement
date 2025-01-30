using InvoiceManagement.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InvoicesController : ControllerBase
{
    private readonly IInvoice _repository;

    public InvoicesController(IInvoice repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices([FromQuery] string status, [FromQuery] string customerName, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var invoices = await _repository.GetInvoicesAsync();

        
        if (!string.IsNullOrEmpty(status))
        {
            invoices = invoices.Where(i => i.Status == status).ToList();
        }
        if (!string.IsNullOrEmpty(customerName))
        {
            invoices = invoices.Where(i => i.CustomerName.Contains(customerName)).ToList();
        }

     
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;

            var pagedInvoices = invoices.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize);

        return Ok(pagedInvoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(int id)
    {
        var invoice = await _repository.GetInvoiceByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
    {
        
        await _repository.AddInvoiceAsync(invoice);

        
        var auditTrail = new InvoiceAuditTrail
        {
            InvoiceId = invoice.Id,
            ActionType = "Create",  
            OldValue = null,  
            NewValue = JsonConvert.SerializeObject(invoice),  
            ChangedBy = User.Identity.Name, 
            ChangedAt = DateTime.UtcNow 
        };
        await _repository.AddAuditTrailAsync(auditTrail); // Log the action

        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
    }
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
    //{
    //    if (id != invoice.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await _repository.UpdateInvoiceAsync(invoice);
    //    return NoContent();
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
    {
        if (id != invoice.Id)
        {
            return BadRequest();
        }

        var existingInvoice = await _repository.GetInvoiceByIdAsync(id);
        if (existingInvoice == null)
        {
            return NotFound();
        }

     
        var oldInvoice = JsonConvert.SerializeObject(existingInvoice);

        
        await _repository.UpdateInvoiceAsync(invoice);

        
        var auditTrail = new InvoiceAuditTrail
        {
            InvoiceId = existingInvoice.Id,
            ActionType = "Update",  
            OldValue = oldInvoice,
            NewValue = JsonConvert.SerializeObject(invoice), 
            ChangedBy = User.Identity.Name, 
            ChangedAt = DateTime.UtcNow  
        };
        await _repository.AddAuditTrailAsync(auditTrail); 

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var invoice = await _repository.GetInvoiceByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }

        var oldInvoice = JsonConvert.SerializeObject(invoice);
        await _repository.DeleteInvoiceAsync(id);

        var auditTrail = new InvoiceAuditTrail
        {
            InvoiceId = id,
            ActionType = "Delete",
            OldValue = oldInvoice,
            NewValue = null,
            ChangedBy = User.Identity.Name,
            ChangedAt = DateTime.UtcNow
        };
        await _repository.AddAuditTrailAsync(auditTrail);

        return NoContent();
    }

}
