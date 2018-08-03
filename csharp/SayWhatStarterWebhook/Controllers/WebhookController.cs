using System.Web.Http;
using SayWhatStarterWebhook.Handlers;
using SayWhatStarterWebhook.Models;

namespace SayWhatStarterWebhook.Controllers
{
    [RoutePrefix("webhook")]
    public class WebhookController : ApiController
    {
        private readonly DialogflowHandler _dialogflowHandler = new DialogflowHandler();

        [Route("dialogflow")]
        [HttpPost]
        public Response HandleDialogflowRequest(Request request)
        {
            return this._dialogflowHandler.Handle(request);
        }
    }
}