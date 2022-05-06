import { FC } from "react";
import { Game, GameProps } from "./Game";

const fakeGame: GameProps = {
    playerId: "Reginald",
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
            playerId: "1",
            playerName: "Harrison1",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "1",
            playerName: "Harrison2",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "2",
            playerName: "Harrison3",
            role: "Murderer",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "3",
            playerName: "Harrison4",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        },
        {
            playerId: "4",
            playerName: "Harrison5",
            role: "Investigator",
            hasBadge: true,
            evidence: ["Game Console", "Apple", "Candle", "Keyboard"],
            meansOfMurder: ["Hatchet", "Strangling", "Injection", "Disease"]
        }
    ]
};

export const GamePreview: FC = () => <Game {...fakeGame} />
