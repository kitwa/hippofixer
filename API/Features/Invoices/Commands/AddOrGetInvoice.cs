using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Invoices.Queries
{
    public class AddOrGetInvoice
    {
        public class Command : IRequest<InvoiceDto>
        {
            public Command(int workorderId, string email)
            {
                WorkorderId = workorderId;
                Email = email;
            }
            public int WorkorderId { get; set; }            
            public string Email {get; set;}
        }

        public class Handler : IRequestHandler<Command, InvoiceDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<InvoiceDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if (command.WorkorderId == 0)
                {
                    throw new ArgumentNullException(nameof(command.WorkorderId));
                }

                var existingInvoice = _context.Invoices  
                                                .Include(x => x.Contractor)
                                                .ThenInclude(x => x.City)                                      
                                                .Include(x => x.InvoiceItems)
                                                .SingleOrDefault(x => x.WorkOrderId == command.WorkorderId);

                if (existingInvoice != null)
                {
                    return _mapper.Map<InvoiceDto>(existingInvoice);
                }
                
                var contractor = _context.Users.FirstOrDefault(x => x.Email == command.Email);

                var newInvoice = new Invoice()
                {
                    WorkOrderId = command.WorkorderId,
                    ContractorId =  contractor.Id
                };

                _context.Invoices.Add(newInvoice);

                await _context.SaveChangesAsync();

                var invoice = _context.Invoices
                                        .Include(x => x.Contractor)  
                                        .Include(x => x.InvoiceItems)
                                        .SingleOrDefault(x => x.WorkOrderId == command.WorkorderId);

                return _mapper.Map<InvoiceDto>(invoice);
            }
        }
    }
}