using API.Data;
using API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Issues.Queries
{
    public abstract class GetIssueTypes
    {
        public class Query : IRequest<List<IssueType>> 
        {
        }

        public class Handler: IRequestHandler<Query, List<IssueType>> 
        {
            private readonly DataContext _context;
            public  Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<IssueType>> Handle(Query query, CancellationToken cancellationToken)
            {
                var issueType = await _context.IssueTypes
                .AsNoTracking()
                .ToListAsync();
                            
                return issueType;
            }
        }
        
    }
}