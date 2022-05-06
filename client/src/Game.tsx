import { FC } from "react";
import { Investigator, InvestigatorComponent } from "./InvestigatorComponent";
import { SceneCard, SceneCardComponent } from "./SceneCardComponent";

export interface GameProps {
    playerId: string,
    role: string,
    forensicScientist: {
        username: string
    },
    sceneCards: SceneCard[],
    investigators: Investigator[]
};

export const Game: FC<GameProps> = (props) => {
    return <div className="flex flex-col min-h-screen">
        <div className="text-light text-2xl bg-trueGray-800 py-8">Your role is: {props.role}</div>
        <div className="my-12">
            <p className="text-light text-2xl mb-8">{props.forensicScientist.username} | Forensic Scientist {props.role === "Forensic Scientist" ? "(You)" : ""} </p>
            <div className="flex-row mb-12">
                {props.sceneCards.map(card => <SceneCardComponent key={card.title} {...card} />)}
            </div>
        </div>
        <div>
            {props.investigators.map(investigator =>
                <InvestigatorComponent
                    key={investigator.playerId}
                    isCurrentPlayer={investigator.playerId === props.playerId}
                    {...investigator} />)}
        </div>
    </div>
};
