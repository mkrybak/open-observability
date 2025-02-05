using OpenObservability.Common.Configurations;
using System;

namespace OpenObservability.Common.Interfaces
{
    public interface IOpenObservabilityBuilder
    {
        public string ServiceName { get; set; }
        public bool StaticTelemetryService { get; set; }
        IOpenObservabilityBuilder WithTracing(Action<TracerConfiguration> configure = default);
        IOpenObservabilityBuilder WithMetrics(Action<MeterConfiguration> configure = default);
        IOpenObservabilityBuilder WithLogs(Action<LoggerConfiguration> configure = default);
    }
}