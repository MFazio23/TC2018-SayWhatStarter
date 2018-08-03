using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models.Messages
{
    public class GoogleBasicCardMessage: GooglePlatformMessage
    {
        public GoogleBasicCard BasicCard { get; set; }

        public static GoogleBasicCardMessage Create(string title, string subtitle, string formattedText,
            BasicCardImage image, BasicCardButton button)
        {
            return new GoogleBasicCardMessage
            {
                BasicCard = new GoogleBasicCard
                {
                    Title = title,
                    Subtitle = subtitle,
                    FormattedText = formattedText,
                    Image = image,
                    Buttons = new List<BasicCardButton> {button}
                }
            };
        }
    }
}