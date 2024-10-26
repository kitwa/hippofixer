using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Issues.Queries
{
    public abstract class GetIssues
    {
        public class Query : IRequest<PagedList<IssueDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<IssueDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<IssueDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var issues = _context.Issues.Where(x => x.StatusId == Constants.Status.Pending)
                                        .Include(x => x.IssueType)
                                        .Include(x => x.Status)
                                        .ProjectTo<IssueDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .AsSingleQuery()
                                        .OrderByDescending(x => x.CreatedDate);

                return await PagedList<IssueDto>.CreateAsync(issues, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
    }
}