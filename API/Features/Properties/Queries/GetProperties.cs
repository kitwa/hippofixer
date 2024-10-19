using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetProperties
    {
        public class Query : IRequest<PagedList<PropertyDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<PropertyDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<PropertyDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var properties = _context.Properties.ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                return await PagedList<PropertyDto>.CreateAsync(properties, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
    }
}