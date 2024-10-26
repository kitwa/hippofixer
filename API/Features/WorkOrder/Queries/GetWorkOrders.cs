using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.WorkOrders.Queries
{
    public abstract class GetWorkOrders
    {
        public class Query : IRequest<PagedList<WorkOrderDto>>
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<WorkOrderDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<WorkOrderDto>> Handle(Query query, CancellationToken cancellationToken)
            {

                var workOrders = _context.WorkOrders.Where(x => x.StatusId == Constants.Status.InProgress 
                                                            || x.StatusId == Constants.Status.Completed)
                                        .Include(x => x.Status)
                                        .Include(x => x.Issue)
                                        .ThenInclude(x => x.IssueType)
                                        .ProjectTo<WorkOrderDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .AsSingleQuery()
                                        .OrderByDescending(x => x.CreatedDate)
                                        .ThenByDescending(x => x.StatusId);

                return await PagedList<WorkOrderDto>.CreateAsync(workOrders, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
    }
}