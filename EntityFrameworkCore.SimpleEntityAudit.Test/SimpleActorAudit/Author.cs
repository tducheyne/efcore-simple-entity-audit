using EntityFrameworkCore.SimpleEntityAudit.Abstractions;
using System;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleActorAudit
{
    public partial class SimpleActorAuditTests
    {
        private class Author : ISimpleActorAuditEntity<int>
        {
            public Guid MyProperty { get; set; }

            public int Id { get; set; }
            public string Name { get; set; }

            public int CreatedBy { get; set; }
            public int? LastModifiedBy { get; set; }
        }
    }
}