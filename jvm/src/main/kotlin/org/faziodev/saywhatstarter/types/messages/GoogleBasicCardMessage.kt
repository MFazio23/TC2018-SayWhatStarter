package org.faziodev.saywhatstarter.types.messages

data class GoogleBasicCardMessage(val basicCard: GoogleBasicCard) : GooglePlatformMessage() {
    companion object {
        fun create(
            title: String = "",
            subtitle: String = "",
            formattedText: String = "",
            image: BasicCardImage? = null,
            button: BasicCardButton? = null
        ): GoogleBasicCardMessage = GoogleBasicCardMessage(GoogleBasicCard(
            title,
            subtitle,
            formattedText,
            image,
            if(button != null) listOf(button) else listOf()
        ))
    }
}