using System;

namespace SteelToeBoot
{
    public class FetcherOptions
    {
        public string BaseDataUri { get; set; }

        public TimeSpan FetchInterval { get; set; }
    }
}