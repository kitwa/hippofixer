using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Commands
{
    public class RegisterUser
    {
        public class Command : IRequest<RegisterDto> {
            public Command(RegisterDto registerDto, UserManager<AppUser> userManager)
            {
                RegisterDto = registerDto;
                UserManager = userManager;
            }

            public RegisterDto RegisterDto { get; set; }
            public UserManager<AppUser> UserManager { get; set;}
        }

        public class Handler : IRequestHandler<Command, RegisterDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<RegisterDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.RegisterDto == null)
                {
                    throw new ArgumentNullException(nameof(command.RegisterDto));
                }

                if (await EmailExists(command.RegisterDto.Email)) 
                {
                    throw new Exception("Email Already Taken");
                }

                var randomUserName = $"user_{Guid.NewGuid().ToString().Substring(0, 8)}"; 

                command.RegisterDto.Username = randomUserName;
                command.RegisterDto.GenderId = 1;

                var user = _mapper.Map<AppUser>(command.RegisterDto);

                user.Email = command.RegisterDto.Email.ToLower();
                var userCity = _context.Cities.FirstOrDefault(x => x.Id == command.RegisterDto.CityId).Name;
                user.FullAddress = userCity;

                var result = await command.UserManager.CreateAsync(user, command.RegisterDto.Password);

                if(!result.Succeeded) 
                {
                    throw new Exception("Unable to register user");
                }
                    

                var roleResult =  await command.UserManager.AddToRoleAsync(user, "Client"); 

                if(!roleResult.Succeeded) 
                {
                    throw new Exception("Unable to Add Role");
                }
                           
                var userResult = _mapper.Map<RegisterDto>(user);

                return userResult;
            }
        
            private async Task<bool> EmailExists(string email)
            {
                return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
            }

        }
    }
}