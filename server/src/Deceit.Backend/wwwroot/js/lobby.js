let connection = new signalR.HubConnectionBuilder().withUrl("/pregame").build();


let connectedPlayersElement = document.getElementById("connected-players");
connection.on("LobbyUpdated", (lobby) => {
    connectedPlayersElement.textContent = "";
    lobby.players.forEach(player => {
        let connectedPlayerElement = document.createElement("p");
        connectedPlayersElement.appendChild(connectedPlayerElement);
        let stringContent = player.name;
        if (lobby.forensicScientistId === player.connectionId) {
            stringContent += " | Forensic Scientist";
        }
        connectedPlayerElement.textContent = stringContent;
    });
});

let gameStateElement = document.getElementById("game-state");
connection.on("GameStateUpdated", (gameState) => {
    gameStateElement.textContent = "";
    gameStateElement.textContent = JSON.stringify(gameState, null, 2);
});

let nameElement = document.getElementById("name");
const forensicScientistButton = document.getElementById("forensic-scientist");
const startGameButton = document.getElementById("start-game");

document.getElementById("connect").addEventListener("click", ev => {
    document.getElementById("connect").disabled = true;
    connection.start()
        .then(() => {
            connection.invoke("ConnectPlayer", lobbyId, nameElement.value);
            forensicScientistButton.disabled = false;
            startGameButton.disabled = false;
        });
});

forensicScientistButton.addEventListener("click", () => {
    connection.invoke("SetConnectionToForensicScientist");
});

startGameButton.addEventListener("click", () => {
    connection.invoke("StartGameInLobby");
})
