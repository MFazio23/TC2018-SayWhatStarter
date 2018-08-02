package org.faziodev.saywhatstarter.types

data class Context(val name: String, val lifespanCount: Int, val parameters: Map<String, String>)