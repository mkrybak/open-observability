using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace demo_webapi_c.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoCController : ControllerBase
    {
        private readonly ApiContext _context;
        private string externalUrl = "https://localhost:7069";

        public DemoCController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/c")]
        public int Get()
        {
            return 1;
        }

        [HttpGet]
        [Route("/custom-metrics")]
        public async Task<int> GetCustomActivity()
        {
            HttpClient client = new HttpClient();

            var watch = new Stopwatch();
            watch.Start();
            var responseForecast = await client.GetAsync(externalUrl + "/WeatherForecast");
            watch.Stop();
            var content = await responseForecast.Content.ReadFromJsonAsync<WeatherForecast>();

            Log.Information("get whether call");

            Telemetry.IncreaseCounter(new CounterDetails()
            {
                Name = "external_service_requests_total",
                Labels = new Dictionary<string, object>(){
                    {"provider", "google"},
                    {nameof(content.Summary), content?.Summary},
                    {"status_code", (int)responseForecast.StatusCode}
                }
            });

            Telemetry.RecordHistogram(new HistogramDetails()
            {
                Name = "external_service_request_duration_milliseconds",
                Duration = watch.ElapsedMilliseconds,
                Units = "milliseconds",
                Labels = new Dictionary<string, object>()
                {
                    { "provider", "google" },
                    { nameof(content.Summary), content?.Summary }
                }
            });

            Telemetry.SetGauge(new GaugeDetails<long>()
            {
                Name = "test_memory_usage",
                Value = () =>
                {
                    var currentProcess = Process.GetCurrentProcess();
                    return currentProcess.PrivateMemorySize64;
                }
            });

            return 1;
        }

        [HttpGet]
        [Route("/db-tracing")]
        public async Task<int> GetDbTracing()
        {

            HttpClient client = new HttpClient();

            var responseForecast = await client.GetAsync(externalUrl + "/WeatherForecast");
            var content = await responseForecast.Content.ReadFromJsonAsync<WeatherForecast>();

            //using (var context = new ApiContext())
            //{
            try
            {
                await _context.WeatherForecast.AddAsync(content);
                await _context.SaveChangesAsync();

                var weatherForecast = await _context.WeatherForecast.FirstOrDefaultAsync(x => x.Date == content.Date);
                if (weatherForecast != null)
                {
                    Log.Information("Successful database queries");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //}

            return 1;
        }
    }
}
