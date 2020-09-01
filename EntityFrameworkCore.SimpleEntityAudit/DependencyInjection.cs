using EntityFrameworkCore.SimpleEntityAudit;
using EntityFrameworkCore.SimpleEntityAudit.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseSimpleAudit<TActorKey, TActorProviderImplementation>(this IServiceCollection services)
            where TActorProviderImplementation : class, IActorProvider<TActorKey>
        {
            return services
                .UseSimpleActorAudit<TActorKey, TActorProviderImplementation>()
                .UseSimpleTimeAudit<SystemClock>();
        }

        public static IServiceCollection UseSimpleActorAudit<TActorKey, TImplemenation>(this IServiceCollection services)
            where TImplemenation : class, IActorProvider<TActorKey>
        {
            return services
                .AddScoped<IActorProvider<TActorKey>, TImplemenation>();
        }

        private static IServiceCollection UseSimpleTimeAudit<TImplementation>(this IServiceCollection services)
            where TImplementation : class, IClock
        {
            return services
                .AddSingleton<IClock, TImplementation>();
        }
    }
}