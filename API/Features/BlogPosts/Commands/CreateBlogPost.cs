using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class CreateBlogPost
    {
        public class Command : IRequest<BlogPostDto> {
            public Command(BlogPostDto blogPostDto)
            {
                BlogPostDto = blogPostDto;
            }
            public BlogPostDto BlogPostDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, BlogPostDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BlogPostDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.BlogPostDto == null)
                {
                    throw new ArgumentNullException(nameof(command.BlogPostDto));
                }

                var blogPost = _mapper.Map<BlogPost>(command.BlogPostDto);
                _context.BlogPosts.Add(blogPost);

                await _context.SaveChangesAsync();
                           
                var BlogPostDtoRet = _mapper.Map<BlogPostDto>(blogPost);

                return BlogPostDtoRet;
            }
        }
    }
}