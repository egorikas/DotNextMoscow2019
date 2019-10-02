using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace Third.Controllers
{
    [Route("api/number")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly ITracer _tracer;

        public NumberController(ITracer tracer)
        {
            _tracer = tracer;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            //return $"третий";
            var client = new HttpClient();
            var row = await client.GetStringAsync("http://localhost:8000/api/number");
            return $"третий {row}";
        }
    }
}