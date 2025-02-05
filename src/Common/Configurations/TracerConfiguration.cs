namespace OpenObservability.Common.Configurations
{
    public class TracerConfiguration : BaseConfiguration
    {
        public bool EntityFrameworkCoreTracing { get; set; } = false;
        public bool OtplExporter { get; set; } = true;
    }
}
