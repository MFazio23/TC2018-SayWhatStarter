package org.faziodev.saywhatstarter.types.messages

data class TextMessage(val text: TextMessageText): Message {
    companion object {
        fun create(singleText: String) = TextMessage(TextMessageText(listOf(singleText)))
    }
}

