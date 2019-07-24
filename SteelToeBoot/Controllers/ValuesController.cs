using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SteelToeBoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        FetcherOptions MyOptions { get; set; }

        
        public ValuesController(IOptions<FetcherOptions> myOptions)
        {
            MyOptions = new FetcherOptions
            {
                BaseDataUri = "Hello",
                FetchInterval = new TimeSpan(0, 1, 0, 0)
            };
            //MyOptions = myOptions.Value;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {MyOptions.BaseDataUri, MyOptions.FetchInterval.ToString()};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}