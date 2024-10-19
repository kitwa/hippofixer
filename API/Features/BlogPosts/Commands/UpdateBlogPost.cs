using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class UpdateBlogPost
    {
        public class Command : IRequest<BlogPostUpdateDto> {
            public Command(int id, BlogPostUpdateDto blogPostUpdateDto)
            {
                BlogPostId = id;
                BlogPostUpdateDto = blogPostUpdateDto;
            }
            public BlogPostUpdateDto BlogPostUpdateDto { get; set; }
            public int BlogPostId { get; set; }
        }

        public class Handler : IRequestHandler<Command, BlogPostUpdateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BlogPostUpdateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.BlogPostUpdateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.BlogPostUpdateDto));
                }
                var blogPost = _context.BlogPosts.FirstOrDefault(p => p.Id == command.BlogPostId);

               _mapper.Map(command.BlogPostUpdateDto, blogPost);

                _context.BlogPosts.Update(blogPost);

                await _context.SaveChangesAsync();
                           
                var BlogPostDtoRet = _mapper.Map<BlogPostUpdateDto>(blogPost);

                return BlogPostDtoRet;
            }
        }
    }
}