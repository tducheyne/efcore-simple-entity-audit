using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleTimeAudit
{
    public partial class SimpleTimeAuditTests
    {
        private class Clock : IClock
        {
            public DateTime ReferenceUtcNow { get; }

            public Clock()
            {
                ReferenceUtcNow = DateTime.UtcNow;
            }

            public DateTime UtcNow => ReferenceUtcNow;
        }
    }
}