using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Quotes.Queries
{
    public abstract class GetQuote
    {
        public class Query : IRequest<QuoteDto> 
        {
            public Query (int carId) 
            {
                QuoteId = carId;
            }

            public int QuoteId {get; set;}
        }

        public class Handler: IRequestHandler<Query, QuoteDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QuoteDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var quote = await _context.Quotes.Where(x => x.Id == query.QuoteId)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<QuoteDto>(quote);

                return result;
            }
        }
        
    }
}