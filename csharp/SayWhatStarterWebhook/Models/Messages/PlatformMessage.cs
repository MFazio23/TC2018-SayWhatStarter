namespace SayWhatStarterWebhook.Models.Messages
{
    public abstract class PlatformMessage: IMessage
    {
        public string Platform { get; set; }
    }
}