using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleActorAudit
{
    public partial class SimpleActorAuditTests
    {
        private class AuditDbContext : SimpleActorAuditDbContext<int>
        {
            public DbSet<Author> Authors { get; set; }

            public AuditDbContext(DbContextOptions options, IActorProvider<int> actorProvider)
                : base(options, actorProvider)
            {
            }
        }
    }
}