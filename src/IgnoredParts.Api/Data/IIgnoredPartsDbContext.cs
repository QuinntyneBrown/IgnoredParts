using IgnoredParts.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace IgnoredParts.Api.Data
{
    public interface IIgnoredPartsDbContext
    {
        DbSet<Opportunity> Opportunities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
