using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetUnit
    {
        public class Query : IRequest<UnitDto> 
        {
            public Query (int carId) 
            {
                UnitId = carId;
            }

            public int UnitId {get; set;}
        }

        public class Handler: IRequestHandler<Query, UnitDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UnitDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var unit = await _context.Units.Where(x => x.Id == query.UnitId)
                                                .Include(x => x.Property)
                                                .Include(x => x.Tenant)
                                                .Include(x => x.Photos)
                                                .Include(x => x.WorkOrders)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<UnitDto>(unit);

                return result;
            }
        }
        
    }
}