import { FC } from "react";

export const LobbyPlayerList: FC<{ connectedPlayers: any[], forensicScientistId: string }> = (props) =>
    <>
        {props.connectedPlayers.map(player =>
            <p className="text-xl">
                {`${player.name}${player.connectionId === props.forensicScientistId ? " | Forensic Scientist" : ""}`}
            </p>)}
    </>
