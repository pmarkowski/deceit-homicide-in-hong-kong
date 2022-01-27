import { FC } from "react";

export interface SceneCard {
    title: string,
    options: string[],
    selectedOptionIndex?: number
}

export const SceneCardComponent: FC<SceneCard> = (props) => <div className="rounded-xl bg-yellow-800 inline-block p-10 mx-8 mb-2 text-trueGray-100 text-lg">
    <p className="pb-4 text-2xl font-light">{props.title}</p>
    <div className="divide-y space-y-2">
        {props.options.map((option, index) => <p key={option} className="border-yellow-700">{`${index === props.selectedOptionIndex ? "*" : ""}${option}`}</p>)}
    </div>
</div>;
