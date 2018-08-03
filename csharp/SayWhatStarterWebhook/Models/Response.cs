using System.Collections.Generic;
using Newtonsoft.Json;
using SayWhatStarterWebhook.Converters;
using SayWhatStarterWebhook.Models.Messages;

namespace SayWhatStarterWebhook.Models
{
    public class Response
    {
        public string Source { get; set; }
        public string FulfillmentText { get; set; }
        [JsonProperty(ItemConverterType = typeof(MessageConverter))]
        public IList<IMessage> FulfillmentMessages { get; set; }
        public IDictionary<string, string> Payload { get; set; }
        public IList<Context> OutputContexts { get; set; }
        public EventInput FollowupEventInput { get; set; }
    }
}