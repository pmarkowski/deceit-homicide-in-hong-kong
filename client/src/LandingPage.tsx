import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { TitleLayout } from "./TitleLayout";

export const LandingPage = () => {

    const [isCreatingLobby, setIsCreatingLobby] = useState(false)

    const navigate = useNavigate();

    useEffect(() => {
        const createAndNavigateToLobby = async () => {
            try {
                const response = await axios.post(`${process.env.REACT_APP_SERVER_BASE_URL}/api/lobby`)
                setIsCreatingLobby(false);
                navigate(`/lobby/${response.data.lobbyId}`);
            }
            catch (error) {
                console.log(error);
                alert(error);
                setIsCreatingLobby(false);
            }
        };

        if (isCreatingLobby) {
            createAndNavigateToLobby();
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
