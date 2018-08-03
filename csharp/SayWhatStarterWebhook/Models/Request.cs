using System.Collections.Generic;
using System.Linq;

namespace SayWhatStarterWebhook.Models
{
    public class Request
    {
        public string ResponseId { get; set; }
        public string Session { get; set; }
        public QueryResult QueryResult { get; set; }
        public Dictionary<string, object> OriginalDetectIntentRequest { get; set; }

        public string GetContextParameter(string contextName, string parameterName)
        {
            return this.QueryResult?.OutputContexts
                ?.FirstOrDefault(c => c.Name?.Equals($"{this.Session}/contexts/{contextName}") ?? false)
                ?.Parameters?[parameterName];
        }
    }
}