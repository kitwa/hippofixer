using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.Users.Commands
{
    public class UploadImageUser
    {
        public class Command : IRequest<MemberDto> {
            public Command(int userId, IFormFile file)
            {
                UserId = userId;
                File = file;
            }
            public int UserId { get; set; }
            public IFormFile File { get; set; }

        }

        public class Handler : IRequestHandler<Command, MemberDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IPhotoService _photoService;

            public  Handler(DataContext context, IMapper mapper, IPhotoService photoService)
            {
                _context = context;
                _mapper = mapper;
                _photoService = photoService;
            }
            public async Task<MemberDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.File == null || command.UserId == 0)
                {
                    throw new ArgumentNullException();
                }
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var result = await _photoService.AddPhotoAsync(command.File, Constants.Cloudinary.KibokoPropertyManagerProfileFolder + env + "/" + command.UserId);

                if (result != null || result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var user = _context.Users.FirstOrDefault(p => p.Id == command.UserId);

                    if(user != null && user?.PhotoPublicId != null){
                        DeletePhoto(user.PhotoPublicId);
                    }

                    user.PhotoPublicId =  result.PublicId;
                    user.PhotoUrl = result.SecureUrl.AbsoluteUri;

                    _context.Users.Update(user);

                    await _context.SaveChangesAsync();
                            
                    var MemberDtoRet = _mapper.Map<MemberDto>(user);

                    return MemberDtoRet;
                }else
                {
                    throw(new Exception("Unable to upload Image"));
                }
                // }

            }

            public async void DeletePhoto(string publicId){
                await _photoService.DeletePhotoAsync(publicId);
            }
        }
    }
}