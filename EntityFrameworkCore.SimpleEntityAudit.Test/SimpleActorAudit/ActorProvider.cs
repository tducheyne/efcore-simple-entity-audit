using EntityFrameworkCore.SimpleEntityAudit.Abstractions;

namespace EntityFrameworkCore.SimpleEntityAudit.Test.SimpleActorAudit
{
    public partial class SimpleActorAuditTests
    {
        private class ActorProvider : IActorProvider<int>
        {
            public int Provide()
            {
                return 999;
            }
        }
    }
}