import { FC } from "react";

export interface Investigator {
    playerId: string,
    playerName: string,
    role?: string,
    hasBadge: boolean,
    evidence: string[],
    meansOfMurder: string[]
}

interface InvestigatorProps extends Investigator {
    isCurrentPlayer: boolean,
}

export const InvestigatorComponent: FC<InvestigatorProps> = (props) => <div className="bg-trueGray-800 rounded-xl p-4 inline-block m-8">
    <p className="text-light text-xl mb-8">{`${props.hasBadge ? "🚨" : ""}${props.playerName}${props.role ? ` | ${props.role}` : ""} ${props.isCurrentPlayer ? "(You)" : ""}`}</p>
    <div className="flex justify-evenly">
        {props.evidence.map(evidence => <span key={evidence} className="btn bg-red-700 text-gray-200 mx-4 w-36">{evidence}</span>)}
    </div>
    <div className="flex justify-evenly mt-4">
        {props.meansOfMurder.map(meansOfMurder => <span key={meansOfMurder} className="btn bg-lightBlue-300 mx-4 w-36">{meansOfMurder}</span>)}
    </div>
</div>;
