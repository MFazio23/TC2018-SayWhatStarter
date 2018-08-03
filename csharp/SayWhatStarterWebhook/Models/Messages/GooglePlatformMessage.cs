namespace SayWhatStarterWebhook.Models.Messages
{
    public abstract class GooglePlatformMessage : PlatformMessage
    {
        protected GooglePlatformMessage()
        {
            base.Platform = "ACTIONS_ON_GOOGLE";
        }
    }
}