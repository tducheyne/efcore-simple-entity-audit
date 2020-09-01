using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}