"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/EventsHub").build();

connection.on("ReceiveEvent", function (eventdata) {
    var li = document.createElement("li");
    document.getElementById("eventsList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `Event Recieved: ${eventdata}`;
});

connection.start().then(function () {
    console.log("SignalR Connection Started!");
}).catch(function (err) {
    return console.error(err.toString());
});