using EntityFrameworkCore.SimpleEntityAudit;
using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class SimpleTimeAuditDbContext : DbContext
    {
        private readonly Auditor _auditor;
        private readonly IClock _clock;

        protected SimpleTimeAuditDbContext(DbContextOptions options, IClock clock)
            : base(options)
        {
            _auditor = new Auditor(ChangeTracker);
            _clock = clock;
        }

        public override int SaveChanges()
        {
            _auditor.ApplyAudit<int>(true, false, clock: _clock, actorProvider: null);

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _auditor.ApplyAudit<int>(true, false, clock: _clock, actorProvider: null);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit<int>(true, false, clock: _clock, actorProvider: null);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit<int>(true, false, clock: _clock, actorProvider: null);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}