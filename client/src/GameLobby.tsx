import * as signalR from "@microsoft/signalr";
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { GameLobbyPlayerList } from "./GameLobbyPlayerList";
import { TitleLayout } from "./TitleLayout";

const playerId = crypto.getRandomValues(new Uint32Array(1))[0].toString(16);

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.REACT_APP_SERVER_BASE_URL}/gamelobby?playerId=${playerId}`)
    .build();

const connectionIsConnected = () =>
    (connection?.state === signalR.HubConnectionState.Connected);

enum LobbyState {
    LOADING,
    DOES_NOT_EXIST,
    NOT_JOINABLE,
    JOINABLE
};

export const GameLobby = () => {
    const params = useParams();
    const lobbyId = params.lobbyId

    const [username, setUsername] = useState("");
    const [lobbyData, setLobbyData] = useState<any>(null);

    const cleanUpSignalRConnection = () => {
        connection.off("LobbyUpdated");
        connection.off("StartGame");
        connection.stop();
    }

    const [lobbyState, setLobbyState] = useState<LobbyState>(LobbyState.LOADING);
    useEffect(() => {
        const getLobby = async () => {
            const getLobbyResult = await axios.get(
                `${process.env.REACT_APP_SERVER_BASE_URL}/api/lobby/${lobbyId}`,
                {
                    headers: { "Player-Id": playerId },
                    validateStatus: (status) =>
                        (200 <= status && status < 300) ||
                        status === 404
                });

            if (getLobbyResult.status === 404) {
                setLobbyState(LobbyState.DOES_NOT_EXIST);
                return;
            }

            if (!getLobbyResult.data.isJoinable) {
                setLobbyState(LobbyState.NOT_JOINABLE);
                return;
            }

            connection.on("LobbyUpdated", (lobby) => {
                console.log(lobby);
                setLobbyData(lobby);
            });

            connection.on("StartGame", (gameState) => {
                console.log(gameState);
            });

            connection.start();

            setLobbyState(LobbyState.JOINABLE);
        };

        getLobby();

        return cleanUpSignalRConnection;
    }, [lobbyId]);

    const joinLobby = () => {
        if (connectionIsConnected()) {
            connection.invoke("ConnectPlayer", lobbyId, username);
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

    const renderLobby = (lobbyState: LobbyState) => {
        switch (lobbyState) {
            case LobbyState.LOADING:
                return <span className="text-light text-xl">Loading...</span>;
            case LobbyState.DOES_NOT_EXIST:
                return <>
                    <span className="text-light text-xl">Lobby not found</span>
                    <a href="/" className="btn btn-blue w-full">Return to Main Menu</a>
                </>;
            case LobbyState.NOT_JOINABLE:
                return <span className="text-light text-xl">Cannot join as a new player for a game already in progress</span>;
            case LobbyState.JOINABLE:
                return !lobbyData ?
                    renderJoinLobby() :
                    renderConnectedLobby();
            default:
                break;
        }
    }

    return <TitleLayout>
        {renderLobby(lobbyState)}
    </TitleLayout>;
};
