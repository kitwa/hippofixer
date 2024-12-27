using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.Invoices.Commands
{
    public class AddInvoiceItem
    {
        public class Command : IRequest<InvoiceItemDto> {
            public Command(int invoiceId, InvoiceItemDto invoiceItemDto)
            {
                InvoiceItemDto = invoiceItemDto;
                InvoiceId = invoiceId;
            }
            public InvoiceItemDto InvoiceItemDto { get; set; }
            public int InvoiceId { get; set; }
        }

        public class Handler : IRequestHandler<Command, InvoiceItemDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<InvoiceItemDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.InvoiceItemDto == null)
                {
                    throw new ArgumentNullException(nameof(command.InvoiceItemDto));
                }

                var invoiceItem = new InvoiceItem();
                    invoiceItem.InvoiceId = command.InvoiceId;
                    invoiceItem.Description = command.InvoiceItemDto.Description;
                    invoiceItem.Quantity = command.InvoiceItemDto.Quantity;
                    invoiceItem.Price = command.InvoiceItemDto.Price;

                _context.InvoiceItems.Add(invoiceItem);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<InvoiceItemDto>(invoiceItem);

                return result;
            }
        }
    }
}