package org.faziodev.saywhatstarter.types

data class EventInput(val name: String, val languageCode: String, val parameters: Map<String, String>)