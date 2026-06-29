document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("chatForm");
    const input = document.getElementById("mensajeInput");
    const chatMessages = document.getElementById("chatMessages");
    const enviarBtn = document.getElementById("enviarBtn");
    const conversacionIdInput = document.getElementById("conversacionId");
    const providerSelect = document.getElementById("providerSelect");

    if (!form) {
        return;
    }

    form.addEventListener("submit", async function (e) {
        e.preventDefault();

        const mensaje = input.value.trim();

        if (!mensaje) {
            return;
        }

        const conversacionId = parseInt(conversacionIdInput.value);
        const provider = parseInt(providerSelect.value);

        const bienvenida = chatMessages.querySelector(".conversation-welcome");
        if (bienvenida) {
            bienvenida.remove();
        }

        agregarMensajeUsuario(mensaje);

        input.value = "";
        input.focus();

        enviarBtn.disabled = true;

        const pensandoId = agregarMensajeAsistente("Pensando...");

        try {
            const response = await fetch("/api/chat/enviar", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    conversacionId: conversacionId,
                    mensaje: mensaje,
                    provider: provider
                })
            });

            const textoRespuesta = await response.text();

            if (!response.ok) {
    let errorMensaje = "Error HTTP " + response.status;

    try {
        const errorData = JSON.parse(textoRespuesta);

        if (errorData.error) {
            errorMensaje += ": " + errorData.error;
        }

        if (errorData.detalle) {
            errorMensaje += " - " + errorData.detalle;
        }
    }
    catch {
        errorMensaje += ": " + textoRespuesta;
    }

    throw new Error(errorMensaje);
}

            const data = JSON.parse(textoRespuesta);

            actualizarMensaje(pensandoId, data.respuesta);
            if (data.titulo) {
                const tituloConversacion = document.getElementById("tituloConversacion");

                if (tituloConversacion) {
                    tituloConversacion.innerText = data.titulo;
                }

                const linkConversacion = document.getElementById("conversacion-link-" + conversacionId);

                if (linkConversacion) {
                    linkConversacion.innerText = data.titulo;
                }
            }
        }
        catch (error) {
            actualizarMensaje(
                pensandoId,
                "Ocurrió un error al obtener la respuesta: " + error.message
            );
        }
        finally {
            enviarBtn.disabled = false;
            input.focus();
        }
    });

    function agregarMensajeUsuario(texto) {
        const div = document.createElement("div");
        div.className = "message-row user-row";

        const contenido = document.createElement("div");
        contenido.className = "message-bubble user-bubble";
        contenido.innerText = texto;

        div.appendChild(contenido);

        const avatar = document.createElement("span");
        avatar.className = "message-avatar user-avatar";
        avatar.innerText = "V";
        div.appendChild(avatar);

        chatMessages.appendChild(div);
        scrollAbajo();
    }

    function agregarMensajeAsistente(texto) {
        const id = "msg-" + Date.now();

        const div = document.createElement("div");
        div.className = "message-row bot-row";
        div.id = id;

        const avatar = document.createElement("span");
        avatar.className = "message-avatar bot-avatar";
        avatar.innerText = "N";

        const contenido = document.createElement("div");
        contenido.className = "message-bubble bot-bubble";
        contenido.innerText = texto;

        div.appendChild(avatar);
        div.appendChild(contenido);

        chatMessages.appendChild(div);
        scrollAbajo();

        return id;
    }

    function actualizarMensaje(id, texto) {
        const div = document.getElementById(id);

        if (!div) {
            return;
        }

        const contenido = div.querySelector(".message-bubble");

        if (contenido) {
            contenido.innerText = texto;
        }

        scrollAbajo();
    }

    function scrollAbajo() {
        if (chatMessages) {
            chatMessages.scrollTop = chatMessages.scrollHeight;
        }
    }
});
