using Consul;

namespace SteelToeBoot
{
    public class ConfigServerData
    {
        public string baseDataUri { get; set; }
        public string fetchInterval { get; set; }
    }
}