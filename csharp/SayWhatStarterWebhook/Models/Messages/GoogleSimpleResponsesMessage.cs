using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models.Messages
{
    public class GoogleSimpleResponsesMessage : GooglePlatformMessage
    {
        public GoogleSimpleResponses SimpleResponses { get; set; }

        public static GoogleSimpleResponsesMessage Create(GoogleSimpleResponse response)
        {
            return new GoogleSimpleResponsesMessage
            {
                SimpleResponses = new GoogleSimpleResponses
                {
                    SimpleResponses = new List<GoogleSimpleResponse> {response}
                }
            };
        }
        public static GoogleSimpleResponsesMessage Create(string displayText, string textToSpeech)
        {
            return Create(new GoogleSimpleResponse
            {
                DisplayText = displayText,
                TextToSpeech = textToSpeech
            });
        }
    }
}