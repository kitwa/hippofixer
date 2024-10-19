using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Features.BlogPosts.Commands
{
    public class CreateProperty
    {
        public class Command : IRequest<PropertyDto> {
            public Command(PropertyDto propertyDto)
            {
                PropertyDto = propertyDto;
            }
            public PropertyDto PropertyDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, PropertyDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PropertyDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.PropertyDto == null)
                {
                    throw new ArgumentNullException(nameof(command.PropertyDto));
                }

                var property = _mapper.Map<Property>(command.PropertyDto);
                _context.Properties.Add(property);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<PropertyDto>(property);

                return result;
            }
        }
    }
}