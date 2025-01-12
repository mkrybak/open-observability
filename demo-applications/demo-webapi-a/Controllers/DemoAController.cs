using Microsoft.AspNetCore.Mvc;

namespace demo_webapi_a.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoAController : ControllerBase
    {
        private string bUrl = "https://localhost:7012";
        private string cUrl = "https://localhost:7092";


        [HttpGet]
        [Route("/a")]
        public async Task<HttpContent> GetA()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(bUrl + "/b");

            return result.Content;
        }

        [HttpGet]
        [Route("/a-b-c")]
        public async Task<HttpContent> GetAToBtoC()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(bUrl + "/b-to-c");

            return result.Content;
        }       
        
        [HttpGet]
        [Route("/a-to-b-custom-activity")]
        public async Task<HttpContent> GetCustomBActivity()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(bUrl + "/custom-activity");

            return result.Content;
        }

        [HttpGet]
        [Route("/a-to-c-custom-metrics")]
        public async Task<HttpContent> GetCustomCMetrics()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(cUrl + "/custom-metrics");

            return result.Content;
        }

        [HttpGet]
        [Route("/a-to-c-db-tracing")]
        public async Task<HttpContent> GetCustomCTracing()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(cUrl + "/db-tracing");

            return result.Content;
        }

        [HttpGet]
        [Route("/a-to-b-exception")]

        public async Task<HttpContent> GetBException()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(bUrl + "/exception");

            return result.Content;
        }
    }
}
