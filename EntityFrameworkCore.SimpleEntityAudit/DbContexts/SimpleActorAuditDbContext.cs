using EntityFrameworkCore.SimpleEntityAudit;
using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class SimpleActorAuditDbContext<T> : DbContext
        where T : struct
    {
        private readonly Auditor _auditor;
        private readonly IActorProvider<T> _actorProvider;

        protected SimpleActorAuditDbContext(DbContextOptions options, IActorProvider<T> actorProvider)
            : base(options)
        {
            _auditor = new Auditor(ChangeTracker);
            _actorProvider = actorProvider;
        }

        public override int SaveChanges()
        {
            _auditor.ApplyAudit(false, true, null, _actorProvider);

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _auditor.ApplyAudit(false, true, clock: null, actorProvider: _actorProvider);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit(false, true, null, _actorProvider);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _auditor.ApplyAudit(false, true, null, _actorProvider);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}