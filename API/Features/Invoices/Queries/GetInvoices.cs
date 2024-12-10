using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Invoices.Queries
{
    public abstract class GetInvoices
    {
        public class Query : IRequest<PagedList<InvoiceDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<InvoiceDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<InvoiceDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var invoices = _context.Invoices.ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                return await PagedList<InvoiceDto>.CreateAsync(invoices, query.UserParams.PageNumber, query.UserParams.PageSize);
            }

        }
    }
}