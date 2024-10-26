using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Issues.Commands
{
    public class RejectWorkOrder
    {
        public class Command : IRequest<WorkOrderDto> {
            public Command(int id)
            {
                WorkOrderId = id;
            }
            public int WorkOrderId { get; set; }            
        }

        public class Handler : IRequestHandler<Command, WorkOrderDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<WorkOrderDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.WorkOrderId == 0)
                {
                    throw new ArgumentNullException("WorkOrderId Not Found");
                }

                var workOrder = await _context.WorkOrders.Where(x => x.Id == command.WorkOrderId)
                                                .SingleOrDefaultAsync();

                if(workOrder == null) 
                {
                    throw new ArgumentNullException("WorkOrder Not Found");
                }

                // workOrder.StatusId = Constants.Status.Rejected;

                var issueForWorkOrder = await _context.Issues.Where(x => x.Id == workOrder.IssueId)
                                                .SingleOrDefaultAsync();

                issueForWorkOrder.StatusId = Constants.Status.Pending;

                _context.WorkOrders.Remove(workOrder);

                await _context.SaveChangesAsync();

                // send reject email
                        
                var res = _mapper.Map<WorkOrderDto>(workOrder);

                return res;

            }
        }
    }
}