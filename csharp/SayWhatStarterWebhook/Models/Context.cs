using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models
{
    public class Context
    {
        public string Name { get; set; }
        public int LifespanCount { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
    }
}