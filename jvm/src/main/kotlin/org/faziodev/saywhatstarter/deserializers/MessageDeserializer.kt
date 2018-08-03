package org.faziodev.saywhatstarter.deserializers

import com.google.gson.JsonDeserializationContext
import com.google.gson.JsonDeserializer
import com.google.gson.JsonElement
import org.faziodev.saywhatstarter.types.messages.*
import java.lang.reflect.Type

class MessageDeserializer: JsonDeserializer<Message?> {

    override fun deserialize(json: JsonElement?, typeOfT: Type?, context: JsonDeserializationContext?): Message? {
        val jsonObject = json?.asJsonObject ?: return null
        if(jsonObject.has("text")) return context?.deserialize(jsonObject, TextMessage::class.java)
        if(jsonObject.has("platform")) {
            val platform: String? = jsonObject["platform"]?.asString
            if(platform == "ACTIONS_ON_GOOGLE") {
                val c = when {
                    jsonObject.has("simpleResponses") -> GoogleSimpleResponsesMessage::class.java
                    jsonObject.has("basicCard") -> GoogleBasicCardMessage::class.java
                    jsonObject.has("linkOutSuggestion") -> GoogleLinkOutSuggestionMessage::class.java
                    else -> return null
                }
                return context?.deserialize(jsonObject, c)
            }
        }
        return null
    }
}