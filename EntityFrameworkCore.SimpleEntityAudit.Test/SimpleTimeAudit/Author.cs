using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleTimeAudit
{
    public partial class SimpleTimeAuditTests
    {
        private class Author : ISimpleTimeAuditEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public DateTime CreatedOn { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
    }
}