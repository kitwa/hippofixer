using API.Data;
using API.DTOs;
using API.Extensions;
using API.Features.Invoices.Commands;
using API.Features.Invoices.Queries;
using API.Features.Users.Commands;
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
        private readonly IWebHostEnvironment _env;

        public InvoicesController(DataContext context, IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _context = context;
            _env = env;
        }

        [HttpGet("get-invoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices([FromQuery]UserParams userParams)
        {
            var invoices = await _mediator.Send(new GetInvoices.Query(userParams));
            
            Response.AddPaginationHeader(invoices.CurrentPage, invoices.PageSize, invoices.TotalCount, invoices.TotalPages);    
            return Ok(invoices);
        }

        [HttpGet("{id}/get-invoice")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
        {
            var invoice = await _mediator.Send(new GetInvoice.Query(id));
            if(invoice != null) {
                return invoice;
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost("{workorderId}/add-get-invoice")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(int workorderId)
        {
            var invoice = await _mediator.Send(new AddOrGetInvoice.Command(workorderId, User.GetEmail()));

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

        [HttpPut("{id}/update-invoice-date/{invoiceDate}")]
        public async Task<ActionResult> UpdateInvoiceDate(int id, string invoiceDate)
        {
            var result = await _mediator.Send(new UpdateInvoiceDate.Command(id, invoiceDate));

            if(result == true){
                return Ok(result);
            }
            return BadRequest("Failed to update due date");
        }

        [HttpPut("{id}/update-due-date/{dueDate}")]
        public async Task<ActionResult> UpdateDueDate(int id, string dueDate)
        {
            var result = await _mediator.Send(new UpdateDueDate.Command(id, dueDate));

            if(result == true){
                return Ok(result);
            }
            return BadRequest("Failed to update due date");
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

        [HttpPost("{invoiceId:int}/add-invoice-item")]
        public async Task<ActionResult<InvoiceItemDto>> AddInvoiceItem(int invoiceId, InvoiceItemDto invoiceItemDto)
        {
            var invoiceItem = await _mediator.Send(new AddInvoiceItem.Command(invoiceId, invoiceItemDto));

            if(invoiceItem != null){
                return Ok(invoiceItem);
            }
            return BadRequest("Failed to add item");
        }

        [HttpPost("send-email-invoice")]
        public async Task<ActionResult<IssueDto>> UploadPhotoForIssue(EmailInvoiceDto emailInvoiceDto)
        {
            if (emailInvoiceDto.File == null || emailInvoiceDto.File.Length == 0)
                return BadRequest("No file atatched.");

            var tempDir = Path.Combine(_env.ContentRootPath, "Temp");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            var filePath = Path.Combine(tempDir, emailInvoiceDto.FileName);

            var sendEmail = new SendEmail();
            var result = await sendEmail.SendEmailWithAttachment(emailInvoiceDto, filePath);
            
            // Delete the temporary file
            System.IO.File.Delete(filePath);

            if(result){
                return Ok(result);
            }
            
            return BadRequest("An error occur");
        }
    }

}