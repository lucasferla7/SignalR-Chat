"use strict";

$(document).ready(function () {
    login();
});

const login = () =>
    $("#loginButton").on('click', () => {
        var user = $("#userId").val();
        $.get(`/api/token/getToken/${user}`).done((token) => {
            upSignal(token);
        });
    });

const upSignal = function (token) {
    var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/chat", {
        accessTokenFactory: () => token
    }).build();

    onInitTyping(connection);
    onFinishTytping(connection);
    onReceiveMessage(connection);


    connection.start().then(function () {
        manageForms();
    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#sendButton").on("click", (event) => {
        const $messageInput = $("#messageInput");
        var message = $messageInput.val();
        var userDestinationId = parseInt($("#userDestinationId").val());
        console.log(userDestinationId, message)
        connection.invoke("SendMessageToUser", message, userDestinationId).catch((err) => {
            return console.error(err.toString());
        });
        $messageInput.val('');
        event.preventDefault();
    });

    typingSignal(connection);
}

const onReceiveMessage = (connection) =>
    connection.on("ReceiveMessage", (user, message) => {
        console.log("asdasda 898989")
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = `${user}: ${msg}`;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

const onInitTyping = (connection) =>
    connection.on("InitTyping", (userId, userName) => {
        $("#typingList").append(`
            <li id='${userId}'>
                ${userName} está digitando...
            </li>
        `);
    });

const onFinishTytping = (connection) =>
    connection.on("FinishTyping", (userId) => {
        $("#typingList").find(`#${userId}`).remove();
    });

const typingSignal = (connection) => {
    const $messageInput = $("#messageInput");
    let isTyping = false;

    $messageInput.on('focusin', (e) => {
        isTyping = false;
        if ($(e.target).val().length > 0)
            isTyping = initTyping(connection);

        $(e.target).on("keyup", (x) => {
            var length = $(e.target).val().length;
            console.log(length)
            if (length > 0 && !isTyping)
                isTyping = initTyping(connection);
            else if (length == 0)
                isTyping = finishTyping(connection);
        });
    });

    $messageInput.on('focusout', () => finishTyping(connection));
}

const initTyping = (connection) => {
    connection.invoke("InitTyping");
    return true;
}

const finishTyping = (connection) => {
    connection.invoke("FinishTyping");
    return false;
}

const manageForms = () => {
    $('#loginForm').hide();
    $('#messageForm').show();
}