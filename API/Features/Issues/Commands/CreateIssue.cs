using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.Issues.Commands
{
    public class CreateIssue
    {
        public class Command : IRequest<IssueDto> {
            public Command(IssueDto issueDto)
            {
                IssueDto = issueDto;
            }
            public IssueDto IssueDto { get; set; }
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
                if(command.IssueDto == null)
                {
                    throw new ArgumentNullException(nameof(command.IssueDto));
                }

                var issue = _mapper.Map<Issue>(command.IssueDto);
                _context.Issues.Add(issue);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<IssueDto>(issue);

                return result;
            }
        }
    }
}