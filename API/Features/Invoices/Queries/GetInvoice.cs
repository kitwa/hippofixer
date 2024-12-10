using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Invoices.Queries
{
    public abstract class GetInvoice
    {
        public class Query : IRequest<InvoiceDto> 
        {
            public Query (int invoiceId) 
            {
                InvoiceId = invoiceId;
            }

            public int InvoiceId {get; set;}
        }

        public class Handler: IRequestHandler<Query, InvoiceDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<InvoiceDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var invoice = await _context.Invoices.Where(x => x.Id == query.InvoiceId)
                                                .Include(x => x.InvoiceItems)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<InvoiceDto>(invoice);

                return result;
            }
        }
        
    }
}