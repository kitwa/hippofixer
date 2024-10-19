using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetProperty
    {
        public class Query : IRequest<PropertyDto> 
        {
            public Query (int carId) 
            {
                PropertyId = carId;
            }

            public int PropertyId {get; set;}
        }

        public class Handler: IRequestHandler<Query, PropertyDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PropertyDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var property = await _context.Properties.Where(x => x.Id == query.PropertyId)
                                                .Include(x => x.PropertyManager)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<PropertyDto>(property);

                return result;
            }
        }
        
    }
}