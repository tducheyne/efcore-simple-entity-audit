using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleTimeAudit
{
    public partial class SimpleTimeAuditTests
    {
        private class Clock : IClock
        {
            public Clock()
            {
                UtcNow = DateTime.UtcNow;
            }

            public DateTime UtcNow { get; }
        }
    }
}