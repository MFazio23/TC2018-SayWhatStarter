package org.faziodev.saywhatstarter.types.messages

data class GoogleLinkOutSuggestionMessage(val linkOutSuggestion: GoogleLinkOutSuggestion): GooglePlatformMessage(){
    companion object {
        fun create(name: String, uri: String): GoogleLinkOutSuggestionMessage =
            GoogleLinkOutSuggestionMessage(GoogleLinkOutSuggestion(name, uri))
    }
}
