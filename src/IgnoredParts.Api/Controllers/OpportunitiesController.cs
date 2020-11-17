using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using IgnoredParts.Api.Features.Opportunities;

namespace IgnoredParts.Api.Controllers
{
    [ApiController]
    [Route("api/opportunities")]
    public class OpportunitiesController
    {
        private readonly IMediator _mediator;

        public OpportunitiesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "UpsertOpportunityRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertOpportunity.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertOpportunity.Response>> Upsert([FromBody]UpsertOpportunity.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{opportunityId}", Name = "RemoveOpportunityRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveOpportunity.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{opportunityId}", Name = "GetOpportunityByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOpportunityById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetOpportunityById.Response>> GetById([FromRoute]GetOpportunityById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Opportunity == null)
            {
                return new NotFoundObjectResult(request.OpportunityId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetOpportunitiesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOpportunities.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOpportunities.Response>> Get()
            => await _mediator.Send(new GetOpportunities.Request());           
    }
}
