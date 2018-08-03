using System;
using System.Collections.Generic;
using SayWhatStarterWebhook.Extensions;
using SayWhatStarterWebhook.Models;
using SayWhatStarterWebhook.Models.Messages;

namespace SayWhatStarterWebhook.Handlers
{
    public class DialogflowHandler
    {
        private const string Source = "CSharpDialogflowHandler";
        private const string EndingQuestion = "Where do you want to go today?";

        private readonly Random _random = new Random();

        public Response Handle(Request request)
        {
            switch (request.QueryResult?.Action)
            {
                case "input.welcome": return this.DefaultWelcome();
                case "input.exit": return this.DefaultExit();
                case "basicFulfillment": return this.BasicFulfillment();
                case "contexts": return this.Contexts(request);
                case "contextsYes": return this.ContextsYes(request);
                case "contextsNo": return this.ContextsNo(request);
                case "entitiesParameters": return this.EntitiesParameters(request);
                case "googleAssistant": return this.GoogleAssistant(request);
                default: return this.DefaultFallback();
            }
        }

        private Response Respond(string text, bool end = false, bool noQuestion = false,
            IList<IMessage> messages = null, IList<Context> contexts = null)
        {
            return new Response
            {
                Source = DialogflowHandler.Source,
                FulfillmentText = $"{text}{(noQuestion ? string.Empty : $"  {DialogflowHandler.EndingQuestion}")}",
                FulfillmentMessages = messages,
                OutputContexts = contexts
            };
        }

        private Response DefaultWelcome()
        {
            return this.Respond("Welcome to the Say What? Starter Action on Google (via the C# webhook)!");
        }

        private Response DefaultExit()
        {
            return this.Respond("Farewell from the Say What? Starter Action on Google (via the C# webhook)!", true,
                true);
        }

        private Response DefaultFallback()
        {
            return this.Respond("I have no idea what you just said (via the C# webhook). Can you try again, please?",
                noQuestion: true);
        }

        private Response BasicFulfillment()
        {
            var responses = new List<string>
            {
                "This is a basic response via a C# webhook.  We can send back whatever we want from our endpoint for Dialogflow to say.",
                "In Dialogflow, we can just add multiple responses and Dialogflow will randomize them.  Here, we need to handle that ourselves via our C# webhook code if we care."
            };

            return this.Respond(responses[this._random.Next(responses.Count)]);
        }

        private Response Contexts(Request request)
        {
            var name = request.QueryResult?.Parameters?["firstName"];

            return this.Respond(
                $"Why, hello there, {name}!  Would you like me to tell you all about Contexts?",
                noQuestion: true,
                contexts: new List<Context>
                {
                    new Context
                    {
                        Name = $"{request.Session}/contexts/infoTime",
                        LifespanCount = 1,
                        Parameters = new Dictionary<string, string>
                        {
                            {"timestamp", DateTime.Now.ToLongTimeString()}
                        }
                    }
                }
            );
        }

        private Response ContextsYes(Request request)
        {
            var name = request.QueryResult?.Parameters?["firstName"];
            var timestamp = request.GetContextParameter("infotime", "timestamp");

            return this.Respond(
                $"Just excellent, {name}!  Contexts allow me to remember information between statements.  For example, I remember that your name is {name} and that you told me this information at {timestamp}.");
        }

        private Response ContextsNo(Request request)
        {
            var name = request.QueryResult?.Parameters?["firstName"];
            var timestamp = request.GetContextParameter("infotime", "timestamp");

            return this.Respond(
                $"Oh.  That's too bad, {name}!  At least I remember that you told me your name (which is {name}) at {timestamp}.");
        }

        private Response EntitiesParameters(Request request)
        {
            var room = request.QueryResult?.Parameters?["room"];
            var originalRoom = request.QueryResult?.Parameters?["originalRoom"]?.Replace("?", string.Empty);
            var city = request.QueryResult?.Parameters?["city"];

            if (!string.IsNullOrEmpty(room) && !string.IsNullOrEmpty(city))
            {
                return this.Respond(
                    $"You can even include multiple parameters in a single response.  In this case, a user mentioned both {city} and {room}.");
            }

            if (!string.IsNullOrEmpty(room) && !string.IsNullOrEmpty(originalRoom) && !room.Equivalent(originalRoom))
            {
                return this.Respond(
                    $"If you have a parameter with synonyms, you can also reference \"<parameter>.original\" which refers to what the user actually said rather than just the reference value.  In most cases, they're the same, but sometimes they can differ.  There may be a difference between \"{room}\" and \"{originalRoom}\".");
            }

            if (!string.IsNullOrEmpty(room))
            {
                return this.Respond(
                    $"You can create your own entities of any type to help your Action.  For example, someone can ask about the {room} room at That Conference and we can react accordingly.");
            }

            if (!string.IsNullOrEmpty(city))
            {
                return this.Respond(
                    $"Using the built-in entity \"geo-city\" will allow you to grab out an entered city, such as {city}.");
            }

            return this.Respond(
                "Entities are context variables you can pull out of a user's phrase.  It could be built-in values like cities and countries or you can create your own.");
        }

        private Response GoogleAssistant(Request request)
        {
            var responseType = request.QueryResult?.Parameters?["responseType"];
            var messages = new List<IMessage>
            {
                GoogleSimpleResponsesMessage.Create(
                    "You can set different responses for text and speech.  This will look different than it sounds.",
                    "You can set different responses for speech and text.  This is going to say something different than what it displayed.")
            };

            if (string.IsNullOrEmpty(responseType) || responseType.Equals("textMessage"))
            {
                messages.Add(TextMessage.Create(
                    "The Google Assistant has extra response types for users on a phone, from card types to links to lists."));
            }

            if (string.IsNullOrEmpty(responseType) || responseType.Equals("basicCard"))
            {
                messages.Add(GoogleBasicCardMessage.Create(
                    "Basic Card",
                    "from the C# webhook",
                    "Cards consist of an image, a card title, a card subtitle, and interactive buttons (for sending user queries or opening links).",
                    new BasicCardImage
                    {
                        ImageUri = "https://ms-vscode.gallerycdn.vsassets.io/extensions/ms-vscode/csharp/1.15.2/1526415359369/Microsoft.VisualStudio.Services.Icons.Default",
                        AccessibilityText = "C# Logo"
                    },
                    new BasicCardButton
                    {
                        Title = "Rich Messages (Card)",
                        OpenUriAction = new ButtonOpenUriAction {Uri = "https://dialogflow.com/docs/rich-messages#card"}
                    }
                ));
            }

            if (string.IsNullOrEmpty(responseType) || responseType.Equals("linkOutSuggestion"))
            {
                messages.Add(GoogleLinkOutSuggestionMessage.Create("Rich Messages docs", "https://dialogflow.com/docs/rich-messages"));
            }

            return this.Respond("Responding to a Google Assistant message.", messages: messages);
        }
    }
}