using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Queries
{
    public abstract class GetBlogPosts
    {
        public class Query : IRequest<PagedList<BlogPostDto>> 
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams {get; set;}
        }

        public class Handler: IRequestHandler<Query, PagedList<BlogPostDto>> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<BlogPostDto>> Handle(Query query, CancellationToken cancellationToken)
            {
                var blogPosts = _context.BlogPosts.ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .OrderBy(x => x.Id);
                            
                return await PagedList<BlogPostDto>.CreateAsync(blogPosts, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
        
    }
}