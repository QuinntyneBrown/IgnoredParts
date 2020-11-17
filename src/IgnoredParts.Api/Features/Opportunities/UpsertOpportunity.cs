using FluentValidation;
using MediatR;
using IgnoredParts.Api.Data;
using IgnoredParts.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace IgnoredParts.Api.Features.Opportunities
{
    public class UpsertOpportunity
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Opportunity).NotNull();
                RuleFor(request => request.Opportunity).SetValidator(new OpportunityValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public OpportunityDto Opportunity { get; set; }
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

                var opportunity = await _context.Opportunities.FindAsync(request.Opportunity.OpportunityId);

                if (opportunity == null)
                {
                    opportunity = new Opportunity();
                    await _context.Opportunities.AddAsync(opportunity);
                }


                await _context.SaveChangesAsync(cancellationToken);

			    return new Response() { 
                    Opportunity = opportunity.ToDto()
                };
            }
        }
    }
}
