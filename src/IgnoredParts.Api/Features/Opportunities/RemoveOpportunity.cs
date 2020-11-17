using MediatR;
using IgnoredParts.Api.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IgnoredParts.Api.Features.Opportunities
{
    public class RemoveOpportunity
    {
        public class Request : IRequest<Unit> {  
            public Guid OpportunityId { get; set; }        
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IIgnoredPartsDbContext _context;

            public Handler(IIgnoredPartsDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {
                
                _context.Opportunities.Remove(await _context.Opportunities.FindAsync(request.OpportunityId));
                
                await _context.SaveChangesAsync(cancellationToken);			    
                
                return new Unit();
            }
        }
    }
}
