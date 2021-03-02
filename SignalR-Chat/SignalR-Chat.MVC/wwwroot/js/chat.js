"use strict";
var _token = null;

$(document).ready(function () {
    login();
});

const login = () =>
    $("#loginButton").on('click', () => {
        var user = $("#userId").val();
        $.get(`/api/token/getToken/${user}`).done((token) => {
            _token = token;
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
        event.preventDefault();

        const $messageInput = $("#messageInput");
        var message = $messageInput.val();
        var userDestinationId = parseInt($("#userDestinationId").val());
        connection.invoke("SendMessageToUser", message, userDestinationId).catch((err) => {
            return console.error(err.toString());
        });
        $messageInput.val('');
    });

    typingSignal(connection);
    getAllUsers();
    getAllMessagesByUserId(1);

    $("#userDestinationId").on("change", (e) => getAllMessagesByUserId(parseInt($(e.target).val())));
}

const onReceiveMessage = (connection) =>
    connection.on("ReceiveMessage", (user, message, date) => {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = `${user}: ${msg} em ${convertDate(date)}`;
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

const getAllUsers = async () => {
    $.ajax({
        beforeSend: function (xhrObj) {
            xhrObj.setRequestHeader("Authorization", `Bearer ${_token}`);
        },
        type: "GET",
        url: "/api/user/getall"
    }).done((users) =>
    {
        users.forEach((user) =>
        {
            $("#userDestinationId").append(`
                <option value='${user.id}'>${user.name}</option>
            `);
        })
    }).fail(() => alert("Deu ruim"));
}

const getAllMessagesByUserId = async (userReceiverId) => {
    $.ajax({
        beforeSend: function (xhrObj) {
            xhrObj.setRequestHeader("Authorization", `Bearer ${_token}`);
        },
        type: "GET",
        url: `/api/message/getAllMessagesByUserId/${userReceiverId}`
    }).done((messages) => {
        $("#messagesList").html('');
        messages.forEach((message) => {
            var sendDate = convertDate(message.sendDate);

            $("#messagesList").append( 
                `<li>
                    ${message.userSender.name}: ${message.text} em ${sendDate}
                </li>`);
        });
    }).fail(() => alert("Deu ruim"));
}

const convertDate = (date) => {
    return new Intl.DateTimeFormat('pt-BR',
        { year: 'numeric', month: 'numeric', day: 'numeric', hour12: false, hour: 'numeric', minute: 'numeric', second: 'numeric' })
        .format(new Date(date));
}