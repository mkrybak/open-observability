using Microsoft.AspNetCore.Mvc;

namespace demo_webapi_b.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoBController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IOpenLogger _logger;

        private string cUrl = "https://localhost:7092";
        private string externalUrl = "https://localhost:7069";

        private ActivityDetails activityDetails = new ActivityDetails()
        {
            Name = "GetWeather"
        };

        public DemoBController(ITelemetryService telemetryService, IOpenLogger logger)
        {
            _telemetryService = telemetryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/b")]
        public int Get()
        {
            return 1;
        }

        [HttpGet]
        [Route("/custom-activityDetails")]
        public async Task<int> GetCustomActivity()
        {
            HttpClient client = new HttpClient();

            using (var activity = _telemetryService.StartActivity(activityDetails))
            {
                var response = await client.GetAsync(externalUrl + "/WeatherForecast");
                var content = await response.Content.ReadFromJsonAsync<WeatherForecast>();
                activity.AddTag(nameof(content.Summary), content?.Summary);
                _logger.LogInformation("executed custom activityDetails. Summary: {0} Tc{1}", content?.Summary, content?.TemperatureC);
            }
            return 1;
        }

        [HttpGet]
        [Route("/exception")]
        public async Task<int> GetException()
        {
            HttpClient client = new HttpClient();

            try
            {
                using (var activity = _telemetryService.StartActivity(activityDetails))
                {
                    var response = await client.GetAsync(externalUrl + "/WeatherForecast1");
                    var content = await response.Content.ReadFromJsonAsync<WeatherForecast>();
                    activity.AddTag(nameof(content.Summary), content?.Summary);
                    _logger.LogInformation("executed custom activityDetails. Summary: {0} Tc{1}", content?.Summary,
                        content?.TemperatureC);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            return 1;
        }


        [HttpGet]
        [Route("/b-to-c")]
        public async Task<HttpContent> GetBToC()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(cUrl + "/c");
            return result.Content;
        }
    }
}
