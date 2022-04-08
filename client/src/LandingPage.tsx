import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { TitleLayout } from "./TitleLayout";

export const LandingPage = () => {

    const [isCreatingLobby, setIsCreatingLobby] = useState(false)

    const navigate = useNavigate()

    useEffect(() => {
        if (isCreatingLobby) {
            axios.post(`${process.env.REACT_APP_SERVER_BASE_URL}/api/lobby`)
                .then(response => {
                    setIsCreatingLobby(false);
                    navigate(`/lobby/${response.data.lobbyId}`);
                })
                .catch(error => {
                    console.log(error);
                    alert(error);
                    setIsCreatingLobby(false);
                });
        }
    }, [isCreatingLobby, navigate]);

    const clickCreateLobby = () => {
        setIsCreatingLobby(true);
    };

    return (
        <TitleLayout>
            <button className="btn btn-blue w-full" onClick={clickCreateLobby}>Create Lobby</button>
        </TitleLayout>
    );
}
