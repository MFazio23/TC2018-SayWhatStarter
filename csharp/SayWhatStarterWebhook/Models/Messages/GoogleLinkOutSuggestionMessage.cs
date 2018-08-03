namespace SayWhatStarterWebhook.Models.Messages
{
    public class GoogleLinkOutSuggestionMessage: GooglePlatformMessage
    {
        public GoogleLinkOutSuggestion LinkOutSuggestion { get; set; }

        public static GoogleLinkOutSuggestionMessage Create(string name, string uri)
        {
            return new GoogleLinkOutSuggestionMessage
            {
                LinkOutSuggestion = new GoogleLinkOutSuggestion
                {
                    DestinationName = name,
                    Uri = uri
                }
            };
        }
    }
}