using MediatR;
using IgnoredParts.Api.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IgnoredParts.Api.Features.Opportunities
{
    public class GetOpportunityById
    {
        public class Request : IRequest<Response> {  
            public Guid OpportunityId { get; set; }        
        }

        public class Response
        {
            public OpportunityDto Opportunity { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IIgnoredPartsDbContext _context;

            public Handler(IIgnoredPartsDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Opportunity = (await _context.Opportunities.FindAsync(request.OpportunityId)).ToDto()
                };
            }
        }
    }
}
