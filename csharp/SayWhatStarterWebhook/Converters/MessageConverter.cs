using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SayWhatStarterWebhook.Models.Messages;

namespace SayWhatStarterWebhook.Converters
{
    public class MessageConverter : JsonConverter<IMessage>
    {
        public override void WriteJson(JsonWriter writer, IMessage value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override IMessage ReadJson(JsonReader reader, Type objectType, IMessage existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            if (obj.ContainsKey("text")) return JsonConvert.DeserializeObject<TextMessage>(obj.ToString());
            if (obj.ContainsKey("platform"))
            {
                var platform = obj.GetValue("platform").ToString();
                switch (platform)
                {
                    case "ACTIONS_ON_GOOGLE" when obj.ContainsKey("simpleResponses"):
                        return JsonConvert.DeserializeObject<GoogleSimpleResponsesMessage>(obj.ToString());
                    case "ACTIONS_ON_GOOGLE" when obj.ContainsKey("basicCard"):
                        return JsonConvert.DeserializeObject<GoogleBasicCardMessage>(obj.ToString());
                    case "ACTIONS_ON_GOOGLE" when obj.ContainsKey("linkOutSuggestion"):
                        return JsonConvert.DeserializeObject<GoogleLinkOutSuggestionMessage>(obj.ToString());
                }
            }

            return null;
        }
    }
}