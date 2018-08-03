using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models
{
    public class EventInput
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
    }
}