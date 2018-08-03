using System.Collections.Generic;

namespace SayWhatStarterWebhook.Models.Messages
{
    public class GoogleBasicCard
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string FormattedText { get; set; }
        public BasicCardImage Image { get; set; }
        public IList<BasicCardButton> Buttons { get; set; }
    }
}