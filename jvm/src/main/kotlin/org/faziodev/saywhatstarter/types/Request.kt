package org.faziodev.saywhatstarter.types

data class Request(
    val responseId: String,
    val session: String,
    val queryResult: QueryResult?,
    val originalDetectIntentRequest: Map<String, Any>?
) {
    fun getContextParameter(contextName: String, parameterName: String): String =
        this.queryResult?.outputContexts?.firstOrNull { it.name == "$session/contexts/$contextName"}?.parameters?.get(parameterName) ?: ""
}