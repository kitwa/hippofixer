using MediatR;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Cards.Commands
{
    public class CreateCard
    {
        public class Command : IRequest<CardDto>
        {
            public CardDto CardDto { get; set; }
            public int UserId { get; set; }

            public Command(CardDto cardDto, int userId)
            {
                CardDto = cardDto;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Command, CardDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CardDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.Cards)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                var card = _mapper.Map<Card>(request.CardDto);
                card.AppUserId = user.Id;

                user.Cards.Add(card);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    throw new Exception("Failed to save the card.");
                }

                return _mapper.Map<CardDto>(card);
            }
        }
    }
}
