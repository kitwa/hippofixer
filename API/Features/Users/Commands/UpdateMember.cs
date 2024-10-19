using API.Data;
using API.DTOs;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class UpdateMember
    {
        public class Command : IRequest<MemberUpdateDto> {
            public Command(string email, MemberUpdateDto memberUpdateDto)
            {
                MemberUpdateDto = memberUpdateDto;
                Email = email;
            }
            public string Email { get; set; }
            public MemberUpdateDto MemberUpdateDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, MemberUpdateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<MemberUpdateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.MemberUpdateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.MemberUpdateDto));
                }
                var user = _context.Users.FirstOrDefault(p => p.Email == command.Email);

               _mapper.Map(command.MemberUpdateDto, user);

                _context.Users.Update(user);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<MemberUpdateDto>(user);

                return result;
            }
        }
    }
}