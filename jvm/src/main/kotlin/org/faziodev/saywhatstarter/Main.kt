package org.faziodev.saywhatstarter

import com.github.salomonbrys.kotson.*
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import io.javalin.ApiBuilder.post
import io.javalin.Javalin
import io.javalin.translator.json.JavalinJacksonPlugin
import org.faziodev.saywhatstarter.deserializers.MessageDeserializer
//import org.faziodev.saywhatstarter.deserializers.MessageDeserializer
import org.faziodev.saywhatstarter.types.Request
import org.faziodev.saywhatstarter.types.Response
import org.faziodev.saywhatstarter.types.messages.Message

fun main(args: Array<String>) {
    val app = Javalin.create().apply {
        port(2323)
        exception(Exception::class.java) { e, _ -> e.printStackTrace() }
        error(404) { ctx -> ctx.json("Not found")}
    }.start()

    val deserializer: Gson = GsonBuilder()
        .registerTypeAdapter(Message::class.java, MessageDeserializer())
        .create()
    val serializer = Gson()

    app.routes {
        post("/") { ctx ->
            val request = deserializer.fromJson<Request>(ctx.body())
            val response: Response = DialogflowHandler.handle(request)
            val result = serializer.toJson(response)
            ctx.result(result)
        }
    }

}