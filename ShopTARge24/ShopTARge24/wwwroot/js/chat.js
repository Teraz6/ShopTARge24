"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Hide button until connection has been made
document.getElementById("sendButton").disable = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");

    document.getElementById("messageList").appendChild(li);
    li.textContent
})