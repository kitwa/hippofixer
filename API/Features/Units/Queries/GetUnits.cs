using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetUnits
    {
        public class Query : IRequest<PagedList<UnitDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<UnitDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<UnitDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var units = _context.Units.Include(x => x.Property)
                                        .ProjectTo<UnitDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                return await PagedList<UnitDto>.CreateAsync(units, query.UserParams.PageNumber, query.UserParams.PageSize);
            }

        }
    }
}