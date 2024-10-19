using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class UpdateUnit
    {
        public class Command : IRequest<UnitUpdateDto> {
            public Command(int id, UnitUpdateDto carUpdateDto)
            {
                UnitId = id;
                UnitUpdateDto = carUpdateDto;
            }
            public UnitUpdateDto UnitUpdateDto { get; set; }
            public int UnitId { get; set; }
        }

        public class Handler : IRequestHandler<Command, UnitUpdateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<UnitUpdateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.UnitUpdateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.UnitUpdateDto));
                }
                var unit = _context.Units.FirstOrDefault(p => p.Id == command.UnitId);

               _mapper.Map(command.UnitUpdateDto, unit);

                _context.Units.Update(unit);

                await _context.SaveChangesAsync();
                           
                var carDtoRet = _mapper.Map<UnitUpdateDto>(unit);

                return carDtoRet;
            }
        }
    }
}