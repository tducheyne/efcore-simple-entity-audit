using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleTimeAudit
{
    public partial class SimpleTimeAuditTests
    {
        private class AuditDbContext : SimpleTimeAuditDbContext
        {
            public DbSet<Author> Authors { get; set; }

            public AuditDbContext(DbContextOptions<AuditDbContext> options, IClock clock)
                : base(options, clock)
            {
            }
        }
    }
}