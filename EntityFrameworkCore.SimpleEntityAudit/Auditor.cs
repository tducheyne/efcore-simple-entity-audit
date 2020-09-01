using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit
{
    internal sealed class Auditor
    {
        private readonly ChangeTracker _changeTracker;

        public Auditor(ChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;
        }

        public void ApplyAudit<T>(
            bool applyTimeAudit,
            bool applyActorAudit,
            IClock clock = null,
            IActorProvider<T> actorProvider = null)
        {
            var auditableEntityEntries = _changeTracker.Entries<IBaseSimpleAuditEntity>();

            foreach (var entry in auditableEntityEntries)
            {
                if (applyTimeAudit) ApplyTimeAudit(entry, clock);
                if (applyActorAudit) ApplyActorAudit(entry, actorProvider);
            }
        }

        private void ApplyTimeAudit(EntityEntry<IBaseSimpleAuditEntity> entry, IClock clock)
        {
            if (clock == null) throw new ArgumentNullException(nameof(clock));
            if (!(entry.Entity is ISimpleTimeAuditEntity simpleTimeAuditEntity)) return;

            switch (entry.State)
            {
                case EntityState.Added:
                    simpleTimeAuditEntity.CreatedOn = clock.UtcNow;
                    break;

                case EntityState.Modified:
                    simpleTimeAuditEntity.LastModifiedOn = clock.UtcNow;
                    break;
            }
        }

        private void ApplyActorAudit<T>(EntityEntry<IBaseSimpleAuditEntity> entry, IActorProvider<T> actorProvider)
        {
            if (actorProvider == null) throw new ArgumentNullException(nameof(actorProvider));
            if (!(entry.Entity is ISimpleActorAuditEntity<T> simpleTimeAuditEntity)) return;

            switch (entry.State)
            {
                case EntityState.Added:
                    simpleTimeAuditEntity.CreatedBy = actorProvider.Provide();
                    break;

                case EntityState.Modified:
                    simpleTimeAuditEntity.LastModifiedBy = actorProvider.Provide();
                    break;
            }
        }
    }
}