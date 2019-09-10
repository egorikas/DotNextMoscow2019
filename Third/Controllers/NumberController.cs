using System;
using System.Collections.Generic;
using System.Linq;
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
            var builder = _tracer.BuildSpan("Получить 3 значение");
            using (var started = builder.StartActive())
            {
                started.Span.Log("мы здесь");
                return $"третий";
            }
        }
    }
}