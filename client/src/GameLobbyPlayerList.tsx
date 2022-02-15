import { FC } from "react";

export const GameLobbyPlayerList: FC<{ connectedPlayers: any[], forensicScientistId: string }> = (props) =>
    <>
        {props.connectedPlayers.map(player =>
            <p className="text-xl" key={player.connectionId}>
                {`${player.name}${player.connectionId === props.forensicScientistId ? " | Forensic Scientist" : ""}`}
            </p>)}
    </>
