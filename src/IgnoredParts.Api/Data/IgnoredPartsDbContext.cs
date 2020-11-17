using IgnoredParts.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IgnoredParts.Api.Data
{
    public class IgnoredPartsDbContext: DbContext, IIgnoredPartsDbContext
    {
        public IgnoredPartsDbContext(DbContextOptions options)
            :base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Opportunity> Opportunities { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
