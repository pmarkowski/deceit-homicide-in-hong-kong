import { useState } from "react";
import { useParams } from "react-router";
import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.REACT_APP_SERVER_BASE_URL}/pregame`)
    .build();

export const Lobby = () => {
    const params = useParams();

    const [username, setUsername] = useState("");

    const joinLobby = () => {
        console.log(username);
        connection.start()
            .then(() => {
                connection.invoke("ConnectPlayer", params.lobbyId, username);
            });
    };

    return <div>
        <h1>Connecting to {params.lobbyId}</h1>
        <label>
            Username:
            <input
                className="border border-black"
                value={username}
                onChange={event => setUsername(event.currentTarget.value)} />
        </label>
        <button className="block btn bg-gray-300" onClick={joinLobby}>Join lobby</button>
    </div>;
};
