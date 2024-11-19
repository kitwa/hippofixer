using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Issues.Commands
{
    public class AcceptIssue
    {
        public class Command : IRequest<IssueDto> {
            public Command(int id, string email)
            {
                IssueId = id;
                Email = email;
            }
            public int IssueId { get; set; }            
            public string Email {get; set;}
        }

        public class Handler : IRequestHandler<Command, IssueDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IssueDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.IssueId == 0)
                {
                    throw new ArgumentNullException();
                }

                var issue = await _context.Issues.Where(x => x.Id == command.IssueId)
                                                .SingleOrDefaultAsync();

                if(issue == null) 
                {
                    throw new ArgumentNullException("Issue Not Found");
                }

                issue.StatusId = Constants.Status.InProgress;
                issue.UpdatedDate = DateTime.Now;

                var contractor = _context.Users.FirstOrDefault(x => x.Email == command.Email);

                WorkOrder workOrder = new WorkOrder();
                workOrder.ContractorId = contractor.Id;
                workOrder.IssueId = issue.Id;
                workOrder.StatusId = Constants.Status.InProgress;
                workOrder.CreatedDate = DateTime.Now;
                workOrder.UpdatedDate = DateTime.Now;

                _context.WorkOrders.Add(workOrder);

                await _context.SaveChangesAsync();

                // send issue accepted email
                        
                var res = _mapper.Map<IssueDto>(issue);

                return res;

            }
        }
    }
}