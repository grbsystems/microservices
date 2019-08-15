using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace SteelToeBoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IOptionsSnapshot<ConfigServerData> IConfigServerData { get; set; }

        private ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }

        private IConfigurationRoot Config { get; set; }
        
        public ValuesController(IConfigurationRoot config, IOptionsSnapshot<ConfigServerData> configServerData, IOptions<ConfigServerClientSettingsOptions> confgServerSettings)
        {
            if (configServerData != null)
                IConfigServerData = configServerData;

            // The settings used in communicating with the Spring Cloud Config Server
            if (confgServerSettings != null)
                ConfigServerClientSettingsOptions = confgServerSettings.Value;

            Config = config;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (IConfigServerData != null && IConfigServerData.Value != null)
            {
                var data = IConfigServerData.Value;
                return new string[] {data.baseDataUri, data.fetchInterval};
            }
            
            return new string[] {"Not Set", "Not Set"};
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