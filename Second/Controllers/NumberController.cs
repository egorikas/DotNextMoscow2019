using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Second.Controllers
{
    [Route("api/number")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var client = new HttpClient();
            var row = await client.GetStringAsync("http://localhost:7000/api/number");
            return $"второй {row}";
        }
    }
}