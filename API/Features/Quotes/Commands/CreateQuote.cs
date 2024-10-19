using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;

namespace API.Features.Quotes.Commands
{
    public class CreateQuote
    {
        public class Command : IRequest<QuoteDto> {
            public Command(QuoteDto carDto)
            {
                QuoteDto = carDto;
            }
            public QuoteDto QuoteDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, QuoteDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<QuoteDto> Handle(Command command, CancellationToken cancellationToken)
            {
                if(command.QuoteDto == null)
                {
                    throw new ArgumentNullException(nameof(command.QuoteDto));
                }

                var quote = _mapper.Map<Quote>(command.QuoteDto);
                _context.Quotes.Add(quote);

                await _context.SaveChangesAsync();
                           
                var result = _mapper.Map<QuoteDto>(quote);

                return result;
            }
        }
    }
}