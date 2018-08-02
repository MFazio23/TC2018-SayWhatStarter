package org.faziodev.saywhatstarter.types

import org.faziodev.saywhatstarter.types.messages.Message

data class RequestContent(val text: String, val end: Boolean = false, val noQuestion: Boolean = false, val messages: List<Message> = listOf()/*, val context: Context?*/)