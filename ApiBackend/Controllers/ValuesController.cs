using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#if DEBUG || PERSONAL
    [AllowAnonymous]
#endif
    public class ValuesController : ControllerBase
    {
        private readonly IHttpContextAccessor _HttpContext;
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger, IHttpContextAccessor HttpContext)
        {
            _HttpContext = HttpContext;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public string[] Get()
        {
            string[] collection = new string[]  { "value1", "value2" };

            _logger.LogInformation("Log message in the ::> Get()");
            return collection;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JObject Get2(int id)
        {
            JObject JSON = new JObject();
            JSON.Add("GET", new JObject(new JProperty(new JProperty("id", id))));

            _logger.LogInformation("Log message in the ::> Get2(int id) method");
            return JSON;
        }

        // POST api/values
        [HttpPost]
        public JObject Post([FromBody] string value)
        {
            JObject JSON = new JObject();
            JSON.Add("POST", new JObject(new JProperty(new JProperty("value", value))));

            _logger.LogInformation("Log message in the ::> Post([FromBody] string value) method");
            return JSON;
        }

        // POST api/values/5
        [HttpPost("{id}")]
        public JObject Post2(int id, [FromBody]JObject data)
        {
            JObject JSON = new JObject();
            JSON.Add("POST", new JObject(new JProperty("id", id.ToString()), new JProperty("data", data)));

            _logger.LogInformation("Log message in the ::> Post2(int id, [FromBody]JObject data) method");
            return JSON;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public JObject Put(int id, [FromBody] string value)
        {
            JObject JSON = new JObject();
            JSON.Add("PUT", new JObject(new JProperty("id", id.ToString()), new JProperty("value", value)));

            _logger.LogInformation("Log message in the ::> Put(int id, [FromBody] string value) method");
            return JSON;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public JObject Delete(int id)
        {
            JObject JSON = new JObject();
            JSON.Add("DELETE", new JObject(new JProperty("id", id)));

            _logger.LogInformation("Log message in the ::> Delete(int id) method");
            return JSON;
        }
    }
}
