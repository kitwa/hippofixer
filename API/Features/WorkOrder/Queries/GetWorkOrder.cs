using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.WorkOrders.Queries
{
    public abstract class GetWorkOrder
    {
        public class Query : IRequest<WorkOrderDto> 
        {
            public Query (int workOrderId) 
            {
                WorkOrderId = workOrderId;
            }

            public int WorkOrderId {get; set;}
        }

        public class Handler: IRequestHandler<Query, WorkOrderDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<WorkOrderDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var workOrder = await _context.WorkOrders.Where(x => x.Id == query.WorkOrderId)
                                                .Include(x => x.Contractor)
                                                .Include(x => x.Status)
                                                .Include(x => x.Invoice)
                                                .Include(x => x.Issue)
                                                .ThenInclude(x => x.Client)
                                                .Include(x => x.Issue)
                                                .ThenInclude(x => x.IssueType)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<WorkOrderDto>(workOrder);

                return result;
            }
        }
        
    }
}