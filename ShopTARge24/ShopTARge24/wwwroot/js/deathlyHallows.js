//create connection
var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/userCount", signalR.HttpTransportType.LongPolling.WebSockets).build();

//connect to methods that hub invokes. Receive notification from hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

//Invoke hub methods. Send notification to hub
function newWindowLoadedOnClient() {
    connectionUserCount.invoke("NewWindowLoaded", "Reten").then((value) => console.log(value));
}

//Start the connection
function fulfilled() {
    console.log("Connection to user hub successful.");
    newWindowLoadedOnClient();
}

function rejected() {

}

connectionUserCount.start().then(fulfilled, rejected);