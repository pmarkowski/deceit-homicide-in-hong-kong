import { FC } from "react";
import { Game, GameProps } from "./Game";

const fakeGame: GameProps = {
    role: "Forensic Scientist",
    forensicScientist: {
        username: "Reginald"
    },
    sceneCards: [
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ]
        },
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ]
        },
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ],
            selectedOptionIndex: 3
        },
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ]
        },
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ]
        },
        {
            title: "Cause of Death",
            options: [
                "Suffocation",
                "Severe Injury",
                "Loss of Blood",
                "Illness/Disease",
                "Poisoning",
                "Accident"
            ]
        }
    ],
    investigators: [
        {
            playerId: "Harrison",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "Harrison",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "Harrison",
            role: "Murderer",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "Harrison",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "Harrison",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        }
    ]
};

export const GamePreview: FC = () => <Game {...fakeGame} />
