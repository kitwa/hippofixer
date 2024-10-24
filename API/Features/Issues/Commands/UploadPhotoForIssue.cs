using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.Issues.Commands
{
    public class UploadPhotoForIssue
    {
        public class Command : IRequest<IssueDto> {
            public Command(int id, IFormFile file)
            {
                IssueId = id;
                File = file;
            }
            public int IssueId { get; set; }
            public IFormFile File { get; set; }

        }

        public class Handler : IRequestHandler<Command, IssueDto>
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
            public async Task<IssueDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.File == null || command.IssueId == 0)
                {
                    throw new ArgumentNullException();
                }

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var folderName = Constants.Cloudinary.KibokoFixerIssueFolder + env + '/' + command.IssueId;

                var result = await _photoService.AddPhotoAsync(command.File, folderName);

                if (result != null || result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var issue = _context.Issues.FirstOrDefault(p => p.Id == command.IssueId);

                    if(issue != null && issue?.PhotoPublicId != null){
                        DeletePhoto(issue.PhotoPublicId);
                    }

                    issue.PhotoPublicId =  result.PublicId;
                    issue.PhotoUrl = result.SecureUrl.AbsoluteUri;

                    _context.Issues.Update(issue);

                    await _context.SaveChangesAsync();
                            
                    var res = _mapper.Map<IssueDto>(issue);

                    return res;
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