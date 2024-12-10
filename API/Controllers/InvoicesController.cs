using API.Data;
using API.DTOs;
using API.Extensions;
using API.Features.Invoices.Queries;
using API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InvoicesController : BaseApiController
    {
        public readonly IMediator _mediator;
        private readonly DataContext _context;
        public InvoicesController(DataContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices([FromQuery]UserParams userParams)
        {
            var invoices = await _mediator.Send(new GetInvoices.Query(userParams));
            
            Response.AddPaginationHeader(invoices.CurrentPage, invoices.PageSize, invoices.TotalCount, invoices.TotalPages);    
            return Ok(invoices);
        }

        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
        {
            var invoice = await _mediator.Send(new GetInvoice.Query(id));
            if(invoice != null) {
                return invoice;
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(InvoiceDto unitDto)
        {
            var invoice = await _mediator.Send(new CreateInvoice.Command(unitDto));

            if(invoice != null){
                return Ok(invoice);
            }
            return BadRequest("Failed to create invoice");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInvoice(int id, InvoiceUpdateDto invoiceUpdateDto)
        {
            var invoice = await _mediator.Send(new UpdateInvoice.Command(id, invoiceUpdateDto));

            if(invoice != null){
                return Ok(invoice);
            }
            return BadRequest("Failed to create invoice");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/delete")]
        public async Task<ActionResult> DeleteUnit(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}