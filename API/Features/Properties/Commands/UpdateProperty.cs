using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class UpdateProperty
    {
        public class Command : IRequest<PropertyUpdateDto> {
            public Command(int id, PropertyUpdateDto carUpdateDto)
            {
                PropertyId = id;
                PropertyUpdateDto = carUpdateDto;
            }
            public PropertyUpdateDto PropertyUpdateDto { get; set; }
            public int PropertyId { get; set; }
        }

        public class Handler : IRequestHandler<Command, PropertyUpdateDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PropertyUpdateDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.PropertyUpdateDto == null)
                {
                    throw new ArgumentNullException(nameof(command.PropertyUpdateDto));
                }
                var property = _context.Properties.FirstOrDefault(p => p.Id == command.PropertyId);

               _mapper.Map(command.PropertyUpdateDto, property);

                _context.Properties.Update(property);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<PropertyUpdateDto>(property);

                return result;
            }
        }
    }
}