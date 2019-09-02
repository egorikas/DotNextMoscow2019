using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace First.Controllers
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
            var row = await client.GetStringAsync("http://localhost:6000/api/number");
            return $"первый {row}";
        }
    }
}