const {dialogflow, BasicCard, Button} = require('actions-on-google');
const express = require('express');
const bodyParser = require('body-parser');
const luxon = require('luxon');

const endingQuestion = "What else can I do for you?";

//Utility methods
const getRandomItemFromArray = (array) => array[Math.floor(Math.random() * array.length)];

const respond = (conv, response, end, noQuestion) => {
    if (!response) {
        response = `I'm sorry, I'm having trouble processing your request.`;
        if (end) conv.close(response);
        else conv.ask(`${response}  Please try again.`);
    } else {
        if (end) {
            conv.close(response);
        } else {
            if (typeof(response) === 'string') {
                conv.ask(`${response}${noQuestion ? '' : `  ${endingQuestion}`}`);
            } else {
                if (response.context) {
                    conv.contexts.set(response.context.name, 1, {[response.context.valueName]: response.context.value});
                }
                conv.ask(`${response.text}${noQuestion ? '' : `  ${endingQuestion}`}`);
                if(response.gaResponse) conv.ask(response.gaResponse);
            }
        }
    }
};

const app = dialogflow();
// Intent handlers
app.intent("Default Welcome Intent", conv => respond(conv, `Welcome to the Say What? Starter Action on Google (via the node.js webhook)! How can I help you?`, false, true));
app.intent("Default Exit Intent", conv => respond(conv, `Farewell from the Say What? Starter Action on Google (via the node.js webhook)!`, true, false));
app.intent("Default Fallback Intent", conv => respond(conv, `I have no idea what you just said (via the node.js webhook). Can you try again, please?`));
app.intent("Basic Fulfillment", conv => {
    const response = getRandomItemFromArray([
        `This is a basic response via webhook.  We can send back whatever we want from our endpoint for Dialogflow to say.`,
        `In Dialogflow, we can just add multiple responses and Dialogflow will randomize them.  Here, we need to handle that ourselves via code (if we want to do that.)`
    ]);
    respond(conv, response);
});
app.intent("Contexts", conv => {
    const params = conv.parameters || {};
    respond(conv, {
        text: `Hey there ${params.firstName}!  How about some extra info about contexts?`,
        context: {name: `infoTime`, valueName: 'timestamp', value: luxon.DateTime.local().toLocaleString(luxon.DateTime.TIME_WITH_SECONDS)}
    }, false, true);
});
app.intent("Contexts - yes", conv => {
    const params = conv.parameters || {};
    const infoTimeParams = conv.contexts.get('infotime') ? conv.contexts.get('infotime').parameters || {} : {};
    respond(conv, `Wonderful, ${params.firstName}!   Contexts allow me to remember pieces of information from one statement to the next.  See how I remember your name is ${params.firstName} without you telling me again?  Also, I know you told me at ${infoTimeParams.timestamp}`);
});
app.intent("Contexts - no", conv => {
    const params = conv.parameters || {};
    const infoTimeParams = conv.contexts.get('infotime') ? conv.contexts.get('infotime').parameters || {} : {};
    respond(conv, `Sorry to hear that, ${params.firstName}.  Maybe another time (not the time you told me your name, ${infoTimeParams.timestamp})?`, false, true);
});
app.intent("Entities/Parameters", conv => {
    const params = conv.parameters || {};
    const contextParams = conv.contexts.get('_actions_on_google').parameters || {};

    let responses = [];

    if(params.city && params.room) {
        responses.push(`You can even include multiple parameters in a single response.  In this case, a user mentioned both ${params.city} and ${params.room}.`);
    } else if(params.city) {
        responses.push(`Using the built-in entity "geo-city" will allow you to grab out an entered city, such as ${params.city}.`);
    } else if(params.room) {
        responses.push(
            `You can create your own entities of any type to help your Action.  For example, someone can ask about the ${params.room} room at That Conference and we can react accordingly.`,
            `If you have a parameter with synonyms, you can also reference "parameter.original" which refers to what the user actually said rather than just the reference value.  In most cases, they're the same, but sometimes they can differ.  There may be a difference between "${params.room}" and "${contextParams['room.original']}".`
        );
    } else {
        responses.push(`Entities are context variables you can pull out of a user's phrase.  It could be built-in values like cities and countries or you can create your own.`);
    }

    respond(conv, getRandomItemFromArray(responses));
});
app.intent("Google Assistant", conv => {
    respond(conv, {
        text: `Cards consist of an image, a card title, a card subtitle, and interactive buttons (for sending user queries or opening links).`,
        gaResponse: new BasicCard({
            title: `Google Assistant Response`,
            subtitle: `via a node.js webhook`,
            image: {
                url: `https://nodejs.org/static/images/logos/nodejs-new-pantone-black.png`,
                accessibilityText: `node.js logo`
            },
            buttons: new Button({
                title: `node.js Home`,
                url: `https://nodejs.org/en/`
            })
        })
    });
});

express().use(bodyParser.json(), app).listen(2323);