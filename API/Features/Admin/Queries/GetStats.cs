using API.Data;
using API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Admin.Queries
{
    public abstract class GetStats
    {
        public class Query : IRequest<Model> 
        {
        }

        public class Model 
        {
            public int NumberOfUsers { get; set; }
            public int NumberOfProperties { get; set; }
        }

        public class Handler: IRequestHandler<Query, Model> 
        {
            private readonly DataContext _context;
            public  Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Model> Handle(Query query, CancellationToken cancellationToken)
            {
                var numberOfUsers = await _context.Users.CountAsync();
                var numberOfProperties = await _context.Properties.CountAsync();

                            
                return new Model {
                    NumberOfUsers = numberOfUsers,
                    NumberOfProperties = numberOfProperties,
                };
            }
        }
        
    }
}