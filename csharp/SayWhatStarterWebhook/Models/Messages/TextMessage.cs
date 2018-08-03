using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models.Messages
{
    public class TextMessage: IMessage
    {
        public TextMessageText Text { get; set; }

        public static TextMessage Create(string singleText)
        {
            return new TextMessage
            {
                Text = new TextMessageText
                {
                    Text = new List<string> {singleText}
                }
            };
        }
    }
}