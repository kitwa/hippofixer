using API.Data;
using API.DTOs;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class CreateUnit
    {
        public class Command : IRequest<UnitDto> {
            public Command(UnitDto carDto)
            {
                UnitDto = carDto;
            }
            public UnitDto UnitDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, UnitDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<UnitDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.UnitDto == null)
                {
                    throw new ArgumentNullException(nameof(command.UnitDto));
                }

                var unit = _mapper.Map<Entities.Unit>(command.UnitDto);
                _context.Units.Add(unit);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<UnitDto>(unit);

                return result;
            }
        }
    }
}