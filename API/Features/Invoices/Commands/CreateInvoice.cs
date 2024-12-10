using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;

namespace API.Features.Invoices.Queries
{
    public class CreateInvoice
    {
        public class Command : IRequest<InvoiceDto> {
            public Command(InvoiceDto invoiceDto)
            {
                InvoiceDto = invoiceDto;
            }
            public InvoiceDto InvoiceDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, InvoiceDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<InvoiceDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.InvoiceDto == null)
                {
                    throw new ArgumentNullException(nameof(command.InvoiceDto));
                }

                var invoice = _mapper.Map<Invoice>(command.InvoiceDto);
                _context.Invoices.Add(invoice);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<InvoiceDto>(invoice);

                return result;
            }
        }
    }
}