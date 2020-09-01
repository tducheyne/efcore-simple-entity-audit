namespace EntityFrameworkCore.SimpleEntityAudit.Abstractions
{
    public interface IActorProvider<T>
    {
        T Provide();
    }
}