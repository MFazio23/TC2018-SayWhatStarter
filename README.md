# TC2018-SayWhatStarter
A starter Action on Google for people attending "Say What? Crafting Dialogue for Your Bot" at [That Conference 2018](https://www.thatconference.com/sessions/session/11947).
This could also be helpful for anyone starging with [Dialogflow](https://dialogflow.com/).

## Examples
The following intents are located in the SayWhatStarter.zip, which can be imported into Dialogflow.

### Basic Response
Contains a few simple training phrases with a couple of corresponding text responses.

### Basic Fulfillment
This is similar to the basic response, but the text response is coming from your webhook endpoint rather than Dialogflow.  You can set a webhook URL within the **Fulfillment** section of your agent.

### Entities/Parameters
This example brings in two parameters (`city` and `room`) to be pulled out of statements and used later.
Text responses attached to the intent include:
* A generic response
* A response when a user states a city
* A response when a user states a room
* A response for a room with an explanation about the difference between `$room` and `$room.original`
* A response with both a city and a room

### Contexts
Contexts allow your agent to retain certain pieces of information from intent to intent and configure certain intents to only be triggered when in a given context.
For example, a user can give their current location in one intent and the Dialogflow agent can remember that for a set number of intents after and use that value as needed.  Also, intents listed with the proper input context will only be trigged when that context is activated.
The example here allows a user to cite their name then respond "yes" or "no" to hearing about how contexts work.  In both examples, the agent is able to reply with their name.
One other piece of note here is the prompt associated with the `firstName` parameter.  Since we made this value required, we include an additional prompt to get that before moving onto the normal response.  If the user states their name initially, we can just skip the prompt piece.

### Google Assistant
When using an Action via the Google Assistant on a phone or device with a display, you can send back additional response types.  This includes (but is not limited to) cards, lists, and media content.
The example can show a basic card, a link out suggestion, and one simple response with differing text and speech responses.

### Default Exit Intent
This is an intent added to make leaving the Action a bit easier.  Instead of saying "stop" or "cancel", it adds a number of other phrases allowing the user to leave the current Action.  Note the "Set this intent as end of conversation" flag, which will end the conversation when the intent is triggered.  We can also end a conversation within the webhook.

## Pre-Populated Intents

### Default Welcome Intent
Created and populated automatically by DialogFlow, this intent has the `Welcome` event associated by default.  This is the standard intent used when starting up an Action, usually by saying *"OK Google, talk to \<invocation phrase>"*.  We can respond just like any other intent, either with text responses in Dialogflow, Google Assistant-specific responses, or fulfillment from the webhook.

### Default Fallback Intent
Created and populated automatically by Dialogflow, this is the intent that's trigged when no other intents are matched.  We can respond just like any other intent, either with text responses in Dialogflow, Google Assistant-specific responses, or fulfillment from the webhook.
Note that you can actually add specific phrases in this intent that are supposed to not match any other intent.  For more info, see here: [Negative Examples](https://dialogflow.com/docs/intents#negative_examples)