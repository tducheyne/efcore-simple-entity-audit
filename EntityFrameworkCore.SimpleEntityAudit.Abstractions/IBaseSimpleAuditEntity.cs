using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Abstractions
{
    public interface IBaseSimpleAuditEntity
    {
    }

    public interface ISimpleActorAuditEntity<T> : IBaseSimpleAuditEntity
        where T : struct
    {
        T CreatedBy { get; set; }
        T? LastModifiedBy { get; set; }
    }

    public interface ISimpleTimeAuditEntity : IBaseSimpleAuditEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }

    public interface ISimpleAuditEntity<T> : ISimpleActorAuditEntity<T>, ISimpleTimeAuditEntity
        where T : struct
    {
    }
}