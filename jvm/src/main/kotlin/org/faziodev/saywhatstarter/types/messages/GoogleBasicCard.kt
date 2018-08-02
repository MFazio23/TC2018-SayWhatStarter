package org.faziodev.saywhatstarter.types.messages

data class GoogleBasicCard(
    val title: String,
    val subtitle: String,
    val formattedText: String,
    val image: BasicCardImage?,
    val buttons: List<BasicCardButton>)