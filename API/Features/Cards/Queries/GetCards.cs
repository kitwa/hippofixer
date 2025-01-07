using API.Data;
using API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Features.Cards.Queries
{
    public class GetCards
    {
        public class Query : IRequest<List<Card>>
        {
            public int UserId { get; }

            public Query(int userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, List<Card>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Card>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Cards
                    .Where(card => card.AppUserId == request.UserId)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
