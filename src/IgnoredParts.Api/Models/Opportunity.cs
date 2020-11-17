using System;
using System.ComponentModel.DataAnnotations;

namespace IgnoredParts.Api.Models
{
    public class Opportunity
    {
        [Key]
        public Guid OpportunityId { get; set; }
    }

}
