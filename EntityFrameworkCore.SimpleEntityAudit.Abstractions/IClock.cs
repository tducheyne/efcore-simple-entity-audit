using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Abstractions
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}