# Design

To facilitate a "real time" game, websockets should be used. A connection will be established between clients and servers.
The server will maintain game state, including all hidden information, and enforce rules. The websocket connection will
send events between the client and server to track game interactions.

## Game States

LOBBY - Game has not yet started, players are joining/leaving the room, possibly selecting the Forensic Scientist roles

CRIME - First part of game, Murderer identifies "Means of Murder" and "Key Evidence"
FIRST_EVIDENCE_COLLECTION_LOCATION_OF_CRIME_SELECTION - Cause of Death is placed, Location of Crime is selected, and 4 Scene Tiles drawn (This feels like it might be 3 "sub" states)
FIRST_EVIDENCE_COLLECTION_SELECTION - Forensic Scientists places all 6 bullets on each card, in any order, and is unable to modify them afterward
FIRST_PRESENTATION - Some time is spent where the investigators can deliberate amongst each other, then each investigator has 30 seconds to make a case
SECOND_EVIDENCE_COLLECTION - A new Scene tile is drawn, and the Forensic Scientist replaces one of the four scene tiles
SECOND_EVIDENCE_COLLECTION_SELECTION - Forensic Scientist places a bullet on the new scene card
SECOND_PRESENTATION - Same as before
THIRD_EVIDENCE_COLLECTION - Same as before
THID_EVIDENCE_COLLECTION_SELECTION - same as before
THIRD_PRESENTATION - Same as before
SOLVING_THE_CRIME - Once per game, each investigator can hand in their badge and try to Solve the Crime. A "Means of Murder" and "Key Evidence" is selected by the investigator
INVESTIGATORS_WIN - The crime has been solved by correctly guessing the means of murder and key evidence
MURDERER_WINS - The crime has not been solved by the end of the THIRD_PRESENTATION

## Actions

Actions are submitted by players and move the game state forwards, and contain accompanying data. Only certain Actions are valid for certain states.

### CRIME

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "SELECT_MEANS_OF_MURDER",
    "data": {
        "meansOfMurder": "Brick",
        "keyEvidence": "Cup"
    }
}
```

Only submittable by the Murderer. Progresses the game to FIRST_EVIDENCE_COLLECTION.

### FIRST_EVIDENCE_COLLECTION_LOCATION_OF_CRIME_SELECTION

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "SELECT_LOCATION_OF_CRIME_SCENE_CARD",
    "data": {
        "locationOfCrimeSceneCardId": 1
    }
}
```

Only submittable by the Forensic Scientist. Progresses the game to FIRST_EVIDENCE_COLLECTION_SELECTION.

### FIRST_EVIDENCE_COLLECTION_SELECTION

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "PLACE_SCENE_CARD_BULLET",
    "data": {
        "sceneCardId": 1,
        "selectionIndex": 2
    }
}
```

Only submittable by the Forensic Scientist. Cannot be submitted for a sceneCard with a bullet already on it.

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "END_EVIDENCE_COLLECTION"
}
```

Only submittable by the Forensic Scientist. Cannot be submitted until all scene cards have a bullet on them.

### FIRST_PRESENTATION

System sends out actions every 30 seconds giving each player a chance to talk? <- Further reinforces the concept that some actions get distributed to the users.

### SECOND_EVIDENCE_COLLECTION
### THIRD_EVIDENCE_COLLECTION

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "REPLACE_SCENE_CARD",
    "data": {
        "sceneCardId": 3
    }
}
```

Replaces the drawn scene card with the selected ID. Can only be submitted by the Forensic Scientist.

### SECOND_EVIDENCE_COLLECTION_SELECTION
### THIRD_EVIDENCE_COLLECTION_SELECTION

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "PLACE_SCENE_CARD_BULLET",
    "data": {
        "sceneCardId": 1,
        "selectionIndex": 2
    }
}
```
Same rules. Since all other scene cards should have bullets on them only the

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "END_EVIDENCE_COLLECTION"
}
```

Only submittable by the Forensic Scientist. Cannot be submitted until all scene cards have a bullet on them.

### SECOND_PRESENTATION
### THIRD_PRESENTATION

### SOLVING_THE_CRIME

At any time an investigator can enter this state (except for the CRIME, INVESTIGATORS_WIN, and MURDERER_WINS phases) by dispatching
```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "SOLVE_THE_CRIME"
}

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "SELECT_EVIDENCE",
    "data": {
        "meansOfMurder": "Brick",
        "keyEvidence": "Cup"
    }
}
```

Can only be submitted by a player and investigator that has not yet done so. Means of Murder and Key Evidence must both belong to the same player, and cannot belong to the accuser.

### INVESTIGATORS_WIN
### MURDERER_WINS

```json
{
    "playerId": "123e4567-e89b-12d3-a456-426614174000",
    "action": "RESTART_GAME"
}
```

Brings players back to the LOBBY.

## Game Data Example

```json
{
    "gameState": "LOBBY",
    "players": [
        {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "name": "Leeroy Jenkins",
            "role": "FORENSIC_SCIENTIST"
        },
        {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "name": "Leeroy Jenkins",
            "role": "INVESTIGATOR",
            "triedSolving": false
        },
        {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "name": "Leeroy Jenkins",
            "role": "MURDERER",
            "triedSolving": false
        }
    ],
    "scene": {
        "drawnSceneCards": [
            {
                "title": "Cause of Death",
                "items": [
                    "Suffocation",
                    "Severe Injury",
                    "Loss of Blood",
                    "Illness/Disease",
                    "Poisoning",
                    "Accident"
                ],
                "selectedIndex": 0
            }
        ],
        "remainingSceneCards": [
            ...sceneCards
        ]
    },
    "meansOfMurder": {
        "drawnMeansOfMurderCards": [
            {
                "playerId": "123e4567-e89b-12d3-a456-426614174000",
                "cards": [
                    "Brick",
                    "Bury",
                    "Candlestick",
                    "Chainsaw"
                ]
            }
        ]
    },
    "keyEvidence": {
        "drawnKeyEvidenceCards": [
            {
                "playerId": "123e4567-e89b-12d3-a456-426614174000",
                "cards": [
                    "Cosmetic Mask",
                    "Cotton",
                    "Cup",
                    "Curtains"
                ]
            }
        ]
    },
    "murdererCards": {
        "meansOfMurder": "Brick",
        "keyEvidence": "Cup"
    }
}
```

## Client

The client is responsible only for presenting game state and facilitating interaction with the game. State and data that
is transferred to the client should contain no "spoilers" about the state of the game, including any knowledge of hidden
roles, next cards in decks, etc.

Most of the game probably looks something like...

                    Philip Markowski
                    FORENSIC SCIENTIST
        SCENE ONE   SCENE TWO ...
        Option 1    Option 1
        Option 2    Option 2 *
        Option 3 *  Option 3
        Option 4    Option 4
        Option 5    Option 5
        Option 6    Option 6

                Philip 1                                            Philip 2
MEANS:      Brick, Bury, Candlestick, Chainsaw      MEANS:      Brick, Bury, Candlestick, Chainsaw
EVIDENCE:   Cosmetic Mask, Cotton, Cup, Curtains    EVIDENCE:   Cosmetic Mask, Cotton, Cup, Curtains
                Philip 3                                            Philip 4

MEANS:      Brick, Bury, Candlestick, Chainsaw      MEANS:      Brick, Bury, Candlestick, Chainsaw
EVIDENCE:   Cosmetic Mask, Cotton, Cup, Curtains    EVIDENCE:   Cosmetic Mask, Cotton, Cup, Curtains

                                    _________________________
                                    |                       |
                                    |    SOLVE THE CRIME    |
                                    |_______________________|

Only investigators/murderer gets to see the SOLVE THE CRIME option and only if they haven't done it yet
Forensic Scientist is able to interact with SCENEs they have not yet put a bullet on
Forensic Scientist also gets a lot of interaction points

Okay, so... I was going to initially just make a client then server and hook up events but this seems like
a mistake. I think it's better to just be really explicit about each state and slowly create each state
transition, and work in really nice, quality vertical slices.

Start with just the lobby and getting everyone connected in rooms.
Set the initial game state.
Let the murderer pick a means/evidence.
First evidence collection.

## SignalR

Going to use SignalR for this to keep this in the .NET world for now.

Game lobbies probably translates well to SignalRs concept of groups.

## Lobbies

Players can create lobbies and then hang around in them until a player in a lobby chooses to start the game.
While in the lobby, players can choose who will be the Forensic Scientist role before starting the game.

After starting the game, the Pregame Lobby becomes a Game Lobby? And the game starts?

Pregame Lobbies can change the forensic scientist role around, Game lobbies cannot. Players can no longer
join/leave a lobby once a game has started. So yeah. Different classes.

PregameLobby?
GameLobby?

What do they have in common? Just a list of players? PregameLobby is probably where you can also set up game
rules like Accomplice, Expansion Packs, etc etc etc so I like the split.

PregameHub
GameHub

Starting to see some ways to split this into vertical slices too.

I feel like I want these to be separate Hubs too while we're at it, but that seems weird? Wonder if there's
a built in supported way to do that?

## State Pattern

Started implementing things via State Pattern which in some ways resolves a lot of the issues I had, a Player Connection action is handled differently when in the Lobby state vs any other state.

Not yet clear to me how to handle persistence. Need to be able to write Context in such a way that all data is present for state. Could just store all events and re-create state every time a request goes through?
Serialize context including current state? Maybe Event Sourcing isn't actually right here because I want to be able to stop events from happening (i.e. two players begin to accuse right after the other, only actually want one to go through).
Wait this is still a problem. System can revoke second event? Maybe...?

Iunno. Just keep this in memory right now I don't feel like dealing with it. It's an infrastructure level problem that I don't feel like dealing with.
