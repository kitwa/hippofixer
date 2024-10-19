using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.BlogPosts.Commands
{
    public class SearchUnit
    {
     public class Command : IRequest<PagedList<UnitDto>>
        {
            public Command(SearchUnitDto searchCarDto, UserParams userParams)
            {
                UserParams = userParams;
                SearchUnitDto = searchCarDto;
            }
            public SearchUnitDto SearchUnitDto { get; set; }
            public UserParams UserParams { get; set; }
        }

        public class Handler : IRequestHandler<Command, PagedList<UnitDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<UnitDto>> Handle(Command query, CancellationToken cancellationToken)
            {

                var cars = _context.Units.ProjectTo<UnitDto>(_mapper.ConfigurationProvider)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .AsSingleQuery();

                cars = ApplyFilters(cars, query);


                return await PagedList<UnitDto>.CreateAsync(cars, query.UserParams.PageNumber, query.UserParams.PageSize);
            }

            private IQueryable<UnitDto> ApplyFilters(IQueryable<UnitDto> query, Command command)
            {


                if (!string.IsNullOrWhiteSpace(command.SearchUnitDto.UnitNumber))
                {
                    query = query.Where(x => x.UnitNumber == command.SearchUnitDto.UnitNumber);
                }

                return query;
            }
        }
    }
}