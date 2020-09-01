using EntityFrameworkCore.SimpleEntityAudit;
using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class SimpleAuditDbContext<T> : DbContext
    {
        private readonly Auditor _auditor;
        private readonly IActorProvider<T> _actorProvider;
        private readonly IClock _clock;

        protected SimpleAuditDbContext(DbContextOptions<SimpleAuditDbContext<T>> options, IActorProvider<T> actorProvider, IClock clock)
            : base(options)
        {
            _auditor = new Auditor(ChangeTracker);
            _actorProvider = actorProvider;
            _clock = clock;
        }

        public override int SaveChanges()
        {
            _auditor.ApplyAudit(true, true, _clock, _actorProvider);

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _auditor.ApplyAudit(true, true, _clock, _actorProvider);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit(true, true, _clock, _actorProvider);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit(true, true, _clock, _actorProvider);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}