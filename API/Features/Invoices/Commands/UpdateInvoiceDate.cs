using API.Data;
using API.DTOs;
using AutoMapper;
using MediatR;

namespace API.Features.Invoices.Queries
{
    public class UpdateInvoiceDate
    {
        public class Command : IRequest<bool> {
            public Command(int invoiceId, string invoiceDate)
            {
                InvoiceId = invoiceId;
                InvoiceDate = invoiceDate;
            }
            public string InvoiceDate { get; set; }
            public int InvoiceId { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly DataContext _context;
            public  Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.InvoiceId == 0)
                {
                    throw new ArgumentNullException(nameof(command.InvoiceId));
                }

                var invoice = _context.Invoices.FirstOrDefault(p => p.Id == command.InvoiceId);
                invoice.CreatedDate = DateTime.Parse(command.InvoiceDate);

                _context.Invoices.Update(invoice);

                var res = await _context.SaveChangesAsync();

                if(res == 0){
                    return false;
                }

                return true;
            }
        }
    }
}