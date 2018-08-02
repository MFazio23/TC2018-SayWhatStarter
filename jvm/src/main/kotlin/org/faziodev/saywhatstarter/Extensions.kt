package org.faziodev.saywhatstarter

fun <T> List<T>.random(): T = this.shuffled().first()
fun String.equivalent(s: String): Boolean = this.toLowerCase() == s.toLowerCase()