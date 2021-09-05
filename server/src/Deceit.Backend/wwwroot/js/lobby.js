let connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

let nameElement = document.getElementById("name");
document.getElementById("connect").addEventListener("click", ev => {
    document.getElementById("connect").disabled = true;
    connection.start()
        .then(() => {
            connection.invoke("ConnectPlayer", lobbyId, nameElement.value);
        });
});

let connectedPlayersElement = document.getElementById("connected-players");
connection.on("LobbyUpdated", (players) => {
    connectedPlayersElement.textContent = "";
    players.forEach(player => {
        let connectedPlayerElement = document.createElement("p");
        connectedPlayersElement.appendChild(connectedPlayerElement);
        connectedPlayerElement.textContent = player.name;
    });
});
