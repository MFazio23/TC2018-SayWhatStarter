package org.faziodev.saywhatstarter.types.messages

class GoogleSimpleResponses(val simpleResponses: GoogleSimpleResponsesInner) : GooglePlatformMessage() {
    companion object {
        fun create(responses: List<GoogleSimpleResponse>): GoogleSimpleResponses {
            return GoogleSimpleResponses(
                GoogleSimpleResponsesInner(
                    responses
                )
            )
        }
        fun create(response: GoogleSimpleResponse): GoogleSimpleResponses = this.create(listOf(response))
    }
}

