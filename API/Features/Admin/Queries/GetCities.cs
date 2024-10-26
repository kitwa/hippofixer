using API.Data;
using API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Admin.Queries
{
    public abstract class GetCities
    {
        public class Query : IRequest<List<City>> 
        {
        }

        public class Handler: IRequestHandler<Query, List<City>> 
        {
            private readonly DataContext _context;
            public  Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<City>> Handle(Query query, CancellationToken cancellationToken)
            {
                var cities = await _context.Cities
                .AsNoTracking()
                .ToListAsync();
                            
                return cities;
            }
        }
        
    }
}