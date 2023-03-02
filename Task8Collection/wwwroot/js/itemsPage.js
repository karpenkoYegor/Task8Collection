var connectionRoom = new signalR.HubConnectionBuilder().withUrl("/hubs/comment").build();
var containerDiv = document.getElementById("commentsBody");
var idItem = document.getElementById("idItem").value;

connectionRoom.on("sendMessage", (userName, commentBody) => {
    console.log(111);
    containerDiv.innerHTML += `<h6 class="card-title">${userName}</h6><p class="card-text">${commentBody}</p>`;
});

function fulfilled() {
    console.log("Success");
    connectionRoom.invoke("JoinUser", idItem);
}

function rejected() {

}
connectionRoom.start().then(fulfilled, rejected);