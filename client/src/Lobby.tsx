import { useEffect, useState } from "react";
import { useParams } from "react-router";
import * as signalR from "@microsoft/signalr";
import { TitleLayout } from "./TitleLayout";
import { LobbyPlayerList } from "./LobbyPlayerList";
import { Game, GameProps } from "./Game";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.REACT_APP_SERVER_BASE_URL}/pregame`)
    .build();

const connectionIsConnected = () =>
    (connection?.state === signalR.HubConnectionState.Connected);

export const Lobby = () => {
    const params = useParams();

    const [username, setUsername] = useState("");
    const [lobbyData, setLobbyData] = useState<any>(null);
    const [gameData, setGameData] = useState<GameProps | null>(null);

    useEffect(() => {
        connection.on("LobbyUpdated", (lobby) => {
            setLobbyData(lobby);
        });

        connection.on("StartGame", (gameState) => {
            console.log(gameState);
            setGameData({
                role: gameState.role,
                forensicScientist: {
                    username: lobbyData.players.find((player: any) => player.connectionId === lobbyData.forensicScientistId).name
                },
                sceneCards: [],
                investigators: gameState.investigators.map((investigator: any) => ({
                    playerId: lobbyData.players.find((player: any) => player.connectionId === investigator.playerId).name,
                    role: investigator.role,
                    hasBadge: true,
                    evidence: investigator.evidenceCards,
                    meansOfMurder: investigator.meansOfMurderCards
                }))
            });
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
            connection.invoke("ConnectPlayer", params.lobbyId, username);
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

    return <>
        {gameData ?
            <Game {...gameData} /> :
            <TitleLayout>
                {!lobbyData ?
                    renderJoinLobby() :
                    renderConnectedLobby()
                }
            </TitleLayout>
        }
    </>;
};
