import { useState } from "react";
import { useParams } from "react-router";
import * as signalR from "@microsoft/signalr";
import { TitleLayout } from "./TitleLayout";
import { LobbyPlayerList } from "./LobbyPlayerList";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.REACT_APP_SERVER_BASE_URL}/pregame`)
    .build();

const connectionIsConnectingOrConnected = () =>
    (connection.state === signalR.HubConnectionState.Connecting) ||
    (connection.state === signalR.HubConnectionState.Connected);

export const Lobby = () => {
    const params = useParams();

    const [username, setUsername] = useState("");
    const [lobbyData, setLobbyData] = useState<any>({});

    // This gets invoked a lot. I suspect multiple subscribers are created for this.
    // How connection is handled. Connecting will need to move to a hook.
    // This can also be observed by strange behaviour with the hub, multiple connections
    // are persisted even though this component has been removed.
    connection.on("LobbyUpdated", (lobby) => {
        console.log(lobby);
        setLobbyData(lobby);
    });

    connection.on("StartGame", (gameState) => {
        console.log(gameState);
    });

    const joinLobby = () => {
        console.log(username);
        if (!connectionIsConnectingOrConnected()) {
            connection.start()
                .then(() => {
                    connection.invoke("ConnectPlayer", params.lobbyId, username);
                });
        }
    };

    const setPlayerToForensicScientist = () => {
        connection.invoke("SetConnectionToForensicScientist");
    }

    const startGame = () => {
        connection.invoke("StartGameInLobby");
    }

    const renderJoinLobby = () => <>
        <p>
            <label className="text-light">
                Username:
                <input
                    className="border border-black rounded-lg p-4 bg-gray-300 text-red-700 font-semibold text-center w-full"
                    value={username}
                    onChange={event => setUsername(event.currentTarget.value)} />
            </label>
        </p>
        <button className="btn btn-blue w-full" onClick={joinLobby}>Join lobby</button>
    </>;

    const renderConnectedLobby = () => <>
        <div className="text-light">
            <LobbyPlayerList connectedPlayers={lobbyData.players} forensicScientistId={lobbyData.forensicScientistId} />
        </div>
        <button className="btn btn-blue w-full" onClick={setPlayerToForensicScientist}>I want to be the Forensic Scientist</button>
        <button className="btn btn-blue w-full" onClick={startGame}>Start Game</button>
    </>;

    return <TitleLayout>
        {!connectionIsConnectingOrConnected() ?
            renderJoinLobby() :
            renderConnectedLobby()
        }
    </TitleLayout>;
};
