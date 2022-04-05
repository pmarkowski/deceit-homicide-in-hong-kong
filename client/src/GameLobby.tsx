import { useEffect, useState } from "react";
import { useParams } from "react-router";
import * as signalR from "@microsoft/signalr";
import { TitleLayout } from "./TitleLayout";
import { GameLobbyPlayerList } from "./GameLobbyPlayerList";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.REACT_APP_SERVER_BASE_URL}/gamelobby`)
    .build();

const connectionIsConnected = () =>
    (connection?.state === signalR.HubConnectionState.Connected);

export const GameLobby = () => {
    const params = useParams();

    const playerId = crypto.getRandomValues(new Uint32Array(1))[0].toString(16);
    const [username, setUsername] = useState("");
    const [lobbyData, setLobbyData] = useState<any>(null);

    useEffect(() => {
        connection.on("LobbyUpdated", (lobby) => {
            console.log(lobby);
            setLobbyData(lobby);
        });

        connection.on("StartGame", (gameState) => {
            console.log(gameState);
        });

        connection.start();

        return () => {
            connection.off("LobbyUpdated");
            connection.off("StartGame");
            connection.stop();
        }
    }, []);

    const joinLobby = () => {
        if (connectionIsConnected()) {
            connection.invoke("ConnectPlayer", params.lobbyId, username, playerId);
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
            <GameLobbyPlayerList connectedPlayers={lobbyData.players} forensicScientistId={lobbyData.forensicScientistId} />
        </div>
        <button className="btn btn-blue w-full" onClick={setPlayerToForensicScientist}>I want to be the Forensic Scientist</button>
        <button className="btn btn-blue w-full" onClick={startGame}>Start Game</button>
    </>;

    return <TitleLayout>
        {!lobbyData ?
            renderJoinLobby() :
            renderConnectedLobby()
        }
    </TitleLayout>;
};
