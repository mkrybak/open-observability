using Microsoft.Extensions.DependencyInjection;
using OpenObservability.Common.Interfaces;
using System;

namespace OpenObservability
{
    public static class OpenObservabilityServiceExtensions
    {
        public static IServiceCollection AddOpenObservability(this IServiceCollection serviceCollection,
    Action<IOpenObservabilityBuilder> configure = null)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var serviceCollectionService = scope.ServiceProvider.GetRequiredService<IServiceCollectionService>();
            serviceCollectionService.SetServiceCollection(serviceCollection);

            var observabilityBuilder = scope.ServiceProvider.GetRequiredService<IOpenObservabilityBuilder>();
            if (configure == null)
            {
                observabilityBuilder.WithTracing();
                observabilityBuilder.WithMetrics();
                observabilityBuilder.WithLogs();
            }
            else
            {
                configure(observabilityBuilder);
            }

            return serviceCollection;
        }
    }
}
