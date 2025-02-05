using OpenTelemetry.Metrics;
using System.Collections.Generic;

namespace OpenObservability.Common.Configurations
{
    public class MeterConfiguration : BaseConfiguration
    {
        public bool RuntimeMetrics { get; set; } = true;
        public bool OtplExporter { get; set; } = true;
        public bool HttpRequestsMetrics { get; set; } = true;

        public IList<KeyValuePair<string, MetricStreamConfiguration>> Views { get; set; }
    }
}
