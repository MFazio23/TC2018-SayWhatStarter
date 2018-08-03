using System.Collections.Generic;
using Newtonsoft.Json;
using SayWhatStarterWebhook.Converters;
using SayWhatStarterWebhook.Models.Messages;

namespace SayWhatStarterWebhook.Models
{
    public class QueryResult
    {
        public string QueryText { get; set; }
        public string Action { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public bool AllRequiredParamsPresent { get; set; }
        public string FulfillmentText { get; set; }
        [JsonProperty(ItemConverterType = typeof(MessageConverter))]
        public IList<IMessage> FulfillmentMessages { get; set; }
        public IList<Context> OutputContexts { get; set; }
        public Intent Intent { get; set; }
        public double IntentDetectionConfidence { get; set; }
        public IDictionary<string, string> DiagnosticInfo { get; set; }
        public string LanguageCode { get; set; }
    }
}