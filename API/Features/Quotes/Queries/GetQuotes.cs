using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Quotes.Queries
{
    public abstract class GetQuotes
    {
        public class Query : IRequest<PagedList<QuoteDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<QuoteDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<QuoteDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var quotes = _context.Quotes.ProjectTo<QuoteDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                return await PagedList<QuoteDto>.CreateAsync(quotes, query.UserParams.PageNumber, query.UserParams.PageSize);
            }

        }
    }
}