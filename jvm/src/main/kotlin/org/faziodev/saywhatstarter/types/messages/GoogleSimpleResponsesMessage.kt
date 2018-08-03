package org.faziodev.saywhatstarter.types.messages

class GoogleSimpleResponsesMessage(val simpleResponses: GoogleSimpleResponses) : GooglePlatformMessage() {
    companion object {
        fun create(responses: List<GoogleSimpleResponse>): GoogleSimpleResponsesMessage {
            return GoogleSimpleResponsesMessage(
                GoogleSimpleResponses(
                    responses
                )
            )
        }
        fun create(response: GoogleSimpleResponse): GoogleSimpleResponsesMessage = this.create(listOf(response))
    }
}

