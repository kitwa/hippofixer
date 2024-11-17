using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Issues.Queries
{
    public abstract class GetIssue
    {
        public class Query : IRequest<IssueDto> 
        {
            public Query (int issueId) 
            {
                IssueId = issueId;
            }

            public int IssueId {get; set;}
        }

        public class Handler: IRequestHandler<Query, IssueDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var issue = await _context.Issues.Where(x => x.Id == query.IssueId)
                                                .Include(x => x.IssueType)
                                                .Include(x => x.Status)
                                                .Include(x => x.Client)
                                                .Include(x => x.City)
                                                .SingleOrDefaultAsync();
                            
                var result =  _mapper.Map<IssueDto>(issue);

                return result;
            }
        }
        
    }
}