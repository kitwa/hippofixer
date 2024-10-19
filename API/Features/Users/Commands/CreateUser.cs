using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<MemberCreateDto> {
            public Command(MemberCreateDto memberCreateDto, UserManager<AppUser> userManager)
            {
                MemberCreateDto = memberCreateDto;
                UserManager = userManager;
            }

            public MemberCreateDto MemberCreateDto { get; set; }
            public UserManager<AppUser> UserManager { get; set;}
        }

        public class Handler : IRequestHandler<Command, MemberCreateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<MemberCreateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.MemberCreateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.MemberCreateDto));
                }

                if (await EmailExists(command.MemberCreateDto.Email)) 
                {
                    throw new Exception("Email Already Taken");
                }

                var user = _mapper.Map<AppUser>(command.MemberCreateDto);

                user.Email = command.MemberCreateDto.Email.ToLower();

                var result = await command.UserManager.CreateAsync(user, command.MemberCreateDto.Password);

                if(!result.Succeeded) 
                {
                    throw new Exception("Unable to create user");
                }
                    

                var roleResult =  await command.UserManager.AddToRoleAsync(user, "Agent"); 

                if(!roleResult.Succeeded) 
                {
                    throw new Exception("Unable to Add Role");
                }
                           
                var userResult = _mapper.Map<MemberCreateDto>(user);

                return userResult;
            }
        
            private async Task<bool> EmailExists(string email)
            {
                return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
            }

        }
    }
}