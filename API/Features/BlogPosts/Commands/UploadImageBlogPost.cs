using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class UploadImageBlogPost
    {
        public class Command : IRequest<BlogPostDto> {
            public Command(int id, IFormFile file)
            {
                BlogPostId = id;
                File = file;
            }
            public int BlogPostId { get; set; }
            public IFormFile File { get; set; }

        }

        public class Handler : IRequestHandler<Command, BlogPostDto>
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
            public async Task<BlogPostDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.File == null || command.BlogPostId == 0)
                {
                    throw new ArgumentNullException();
                }

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var folderName = Constants.Cloudinary.KibokoPropertyManagerBlogPostFolder + env + '/' + command.BlogPostId;

                var result = await _photoService.AddPhotoAsync(command.File, folderName);

                if (result != null || result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var blogPost = _context.BlogPosts.FirstOrDefault(p => p.Id == command.BlogPostId);

                    if(blogPost != null && blogPost?.PhotoPublicId != null){
                        DeletePhoto(blogPost.PhotoPublicId);
                    }

                    blogPost.PhotoPublicId =  result.PublicId;
                    blogPost.PhotoUrl = result.SecureUrl.AbsoluteUri;

                    _context.BlogPosts.Update(blogPost);

                    await _context.SaveChangesAsync();
                            
                    var BlogPostDtoRet = _mapper.Map<BlogPostDto>(blogPost);

                    return BlogPostDtoRet;
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