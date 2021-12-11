import { useParams } from "react-router";

export const Lobby = () => {
    const params = useParams();

    return <span>Loading up {params.lobbyId}</span>
};
