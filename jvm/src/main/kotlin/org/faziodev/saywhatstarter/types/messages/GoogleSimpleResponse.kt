package org.faziodev.saywhatstarter.types.messages

data class GoogleSimpleResponse(val textToSpeech: String, val displayText: String? = null)