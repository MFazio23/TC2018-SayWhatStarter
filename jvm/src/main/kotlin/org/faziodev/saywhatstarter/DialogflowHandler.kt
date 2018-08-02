package org.faziodev.saywhatstarter

import org.faziodev.saywhatstarter.types.Context
import org.faziodev.saywhatstarter.types.Request
import org.faziodev.saywhatstarter.types.Response
import org.faziodev.saywhatstarter.types.messages.*
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

object DialogflowHandler {
    private const val source: String = "KotlinDialogflowHandler"
    private const val endingQuestion: String = "Anything else I can help you with?"
    private val dateFormatter: DateTimeFormatter = DateTimeFormatter.ofPattern("h:mm:ss a")

    fun handle(request: Request): Response = when (request.queryResult?.action) {
        "input.welcome" -> defaultWelcome()
        "input.exit" -> defaultExit()
        "basicFulfillment" -> basicFulfillment()
        "contexts" -> contexts(request)
        "contextsYes" -> contextsYes(request)
        "contextsNo" -> contextsNo(request)
        "entitiesParameters" -> entitiesParameters(request)
        "googleAssistant" -> googleAssistant(request)
        else -> defaultFallback()
    }

    private fun respond(text: String, end: Boolean = false, noQuestion: Boolean = false, messages: List<Message> = listOf(), context: List<Context> = listOf()): Response {
        return Response(source, "$text${if (noQuestion) "" else "  $endingQuestion"}", messages, outputContexts = context)
    }

    private fun defaultWelcome(): Response = this.respond("Welcome to the Say What? Starter Action on Google (via the Kotlin webhook)! How can I help you?", noQuestion = true)

    private fun defaultExit(): Response = this.respond("Farewell from the Say What? Starter Action on Google (via the Kotlin webhook)!", true, true)

    private fun defaultFallback(): Response = this.respond("I have no idea what you just said (via the Kotlin webhook). Can you try again, please?", noQuestion = true)

    private fun basicFulfillment(): Response {
        val text: String = listOf(
            "This is a basic response via a Kotlin webhook.  We can send back whatever we want from our endpoint for Dialogflow to say.",
            "In Dialogflow, we can just add multiple responses and Dialogflow will randomize them.  Here, we need to handle that ourselves via our Kotlin webhook code (if we even want to bother doing that.)"
        ).random()
        return this.respond(text)
    }

    private fun contexts(request: Request): Response {
        val name: String = request.queryResult?.parameters?.get("firstName") ?: ""

        return this.respond(
            "Good to see you, $name!  How about some extra info about contexts?",
            noQuestion = true,
            context = listOf(Context(
                "${request.session}/contexts/infoTime",
                1,
                mapOf("timestamp" to LocalDateTime.now().format(dateFormatter))
            ))
        )
    }

    private fun contextsYes(request: Request): Response {
        val name: String = request.queryResult?.parameters?.get("firstName") ?: ""
        val timestamp: String = request.getContextParameter("infotime", "timestamp")
        return this.respond("Wonderful, $name!   Contexts allow me to remember pieces of information from one statement to the next.  See how I remember your name is $name and that you told me at $timestamp without you telling me again?")
    }

    private fun contextsNo(request: Request): Response {
        val name: String = request.queryResult?.parameters?.get("firstName") ?: ""
        val timestamp: String = request.getContextParameter("infotime", "timestamp")
        return this.respond("Sorry to hear that, $name.  At least I still remember that your name is $name and you told me at $timestamp.")
    }

    private fun entitiesParameters(request: Request): Response {
        if (request.queryResult == null) return Response("blah")
        val room: String = request.queryResult.parameters["room"] ?: ""
        val originalRoom: String = request.queryResult.parameters["originalRoom"]?.replace("([.?])*".toRegex(), "")
            ?: ""
        val city: String = request.queryResult.parameters["city"] ?: ""

        val text = when {
            room != "" && city != "" -> "You can even include multiple parameters in a single response.  In this case, a user mentioned both $city and $room."
            room != "" && originalRoom != "" && !room.equivalent(originalRoom) -> "If you have a parameter with synonyms, you can also reference \"<parameter>.original\" which refers to what the user actually said rather than just the reference value.  In most cases, they're the same, but sometimes they can differ.  There may be a difference between \"$room\" and \"$originalRoom\""
            room != "" -> "You can create your own entities of any type to help your Action.  For example, someone can ask about the $room room at That Conference and we can react accordingly."
            city != "" -> "Using the built-in entity \"geo-city\" will allow you to grab out an entered city, such as $city."
            else -> "Entities are context variables you can pull out of a user's phrase.  It could be built-in values like cities and countries or you can create your own."
        }

        return this.respond(text)
    }

    private fun googleAssistant(request: Request): Response {
        val responseType: String? = request.queryResult?.parameters?.get("responseType")
        val messages: MutableList<Message> = mutableListOf(
            GoogleSimpleResponses.create(GoogleSimpleResponse(
                "You can set different responses for speech and text.  This is going to say something different than what it displayed.",
                "You can set different responses for text and speech.  This will look different than it sounds."))
        )
        if (responseType == null || responseType == "textMessage") {
            messages.add(TextMessage.create("The Google Assistant has extra response types for users on a phone, from card types to links to lists."))
        }
        if (responseType == null || responseType == "basicCard") {
            messages.add(GoogleBasicCardMessage.create(
                "Basic Card",
                "Basic Card",
                "Cards consist of an image, a card title, a card subtitle, and interactive buttons (for sending user queries or opening links).",
                BasicCardImage("https://upload.wikimedia.org/wikipedia/commons/b/b5/Kotlin-logo.png", "Kotlin Logo"),
                BasicCardButton("Rich Messages (Card)", ButtonOpenUriAction("https://dialogflow.com/docs/rich-messages#card"))
            ))
        }
        if (responseType == null || responseType == "linkOutSuggestion") {
            messages.add(GoogleLinkOutSuggestionMessage.create("Rich Messages docs", "https://dialogflow.com/docs/rich-messages"))
        }

        return this.respond("GA", messages = messages.toList())
    }

}