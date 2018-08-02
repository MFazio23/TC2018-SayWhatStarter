package org.faziodev.saywhatstarter.types

import org.faziodev.saywhatstarter.types.messages.Message

data class Response(
    val source: String,
    val fulfillmentText: String? = null,
    val fulfillmentMessages: List<Message> = listOf(),
    val payload: Map<String, String>? = mapOf(),
    val outputContexts: List<Context>? = listOf(),
    val followupEventInput: EventInput? = null
)