using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Commands
{
    public class SearchProperty
    {
     public class Command : IRequest<PagedList<PropertyDto>>
        {
            public Command(SearchPropertyDto searchPropertyDto, UserParams userParams)
            {
                UserParams = userParams;
                SearchPropertyDto = searchPropertyDto;
            }
            public SearchPropertyDto SearchPropertyDto { get; set; }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Command, PagedList<PropertyDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<PropertyDto>> Handle(Command query, CancellationToken cancellationToken)
            {

                var properties = _context.Properties.ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                properties = ApplyFilters(properties, query);


                return await PagedList<PropertyDto>.CreateAsync(properties, query.UserParams.PageNumber, query.UserParams.PageSize);
            }

            private IQueryable<PropertyDto> ApplyFilters(IQueryable<PropertyDto> query, Command command)
            {

                if (!string.IsNullOrWhiteSpace(command.SearchPropertyDto.Address))
                {
                    query = query.Where(x => x.Address.Contains(command.SearchPropertyDto.Address));
                }

                return query;
            }
        }
    }
}