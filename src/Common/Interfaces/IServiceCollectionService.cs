using Microsoft.Extensions.DependencyInjection;

namespace OpenObservability.Common.Interfaces
{
    public interface IServiceCollectionService
    {
        public IServiceCollection ServiceCollection { get; }
        public void SetServiceCollection(IServiceCollection serviceCollection);
    }
}
