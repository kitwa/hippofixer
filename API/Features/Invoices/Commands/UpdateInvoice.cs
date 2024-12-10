using API.Data;
using API.DTOs;
using AutoMapper;
using MediatR;

namespace API.Features.Invoices.Queries
{
    public class UpdateInvoice
    {
        public class Command : IRequest<InvoiceUpdateDto> {
            public Command(int id, InvoiceUpdateDto carUpdateDto)
            {
                UnitId = id;
                InvoiceUpdateDto = carUpdateDto;
            }
            public InvoiceUpdateDto InvoiceUpdateDto { get; set; }
            public int UnitId { get; set; }
        }

        public class Handler : IRequestHandler<Command, InvoiceUpdateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<InvoiceUpdateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.InvoiceUpdateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.InvoiceUpdateDto));
                }

                var invoice = _context.Invoices.FirstOrDefault(p => p.Id == command.UnitId);

               _mapper.Map(command.InvoiceUpdateDto, invoice);

                _context.Invoices.Update(invoice);

                await _context.SaveChangesAsync();
                           
                var carDtoRet = _mapper.Map<InvoiceUpdateDto>(invoice);

                return carDtoRet;
            }
        }
    }
}