package org.faziodev.saywhatstarter.types

import org.faziodev.saywhatstarter.types.messages.Message

data class QueryResult(
    val queryText: String,
    val action: String? = "",
    val parameters: Map<String, String>,
    val allRequiredParamsPresent: Boolean,
    val fulfillmentText: String,
    val fulfillmentMessages: List<Message>,
    val outputContexts: List<Context>?,
    val intent: Intent,
    val intentDetectionConfidence: Double,
    val diagnosticInfo: Map<String, String>?,
    val languageCode: String
)