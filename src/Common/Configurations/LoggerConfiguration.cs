using OpenTelemetry.Logs;

namespace OpenObservability.Common.Configurations
{
    public class LoggerConfiguration
    {
        public bool ExtendedLogContext { get; set; } = true;
        public bool EnvironmentName { get; set; } = true;
        public bool ClearDefaultLoggerProviders { get; set; } = true;
        public bool UseStaticLogger { get; set; } = false;
        public bool UseServiceLogger { get; set; } = true;
        public bool RegisterAsDefaultLogger { get; set; } = true;
    }
}
