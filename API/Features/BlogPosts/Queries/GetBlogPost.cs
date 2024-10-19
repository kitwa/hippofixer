using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetBlogPost
    {
        public class Query : IRequest<BlogPostDto> 
        {
            public Query (int blogPostId) 
            {
                BlogPostId = blogPostId;
            }

            public int BlogPostId {get; set;}
        }

        public class Handler: IRequestHandler<Query, BlogPostDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BlogPostDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var blogPost = await _context.BlogPosts.Where(x => x.Id == query.BlogPostId).SingleOrDefaultAsync();
                            
                return _mapper.Map<BlogPostDto>(blogPost);
            }
        }
        
    }
}