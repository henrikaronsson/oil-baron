# Architecture

## Overview

Solution file: `OilBaron.slnx` (.NET 10).

```text
oil-baron-web  --HTTP intents-->  OilBaron.Api  -->  InMemoryGameStore
                                      |
                                      v
                                 OilBaron.Game  (rules + GameState)
```

- **OilBaron.Game** — pure library: create game, apply commands, no I/O.
- **OilBaron.Api** — hosts HTTP, CORS, in-memory dictionary of games by id.
- **oil-baron-web** — Angular UI; displays DTOs; no economy math.

## Why a separate Game project

- Unit tests without spinning up ASP.NET.
- Clear boundary: “if it changes cash/reserves/price, it belongs here.”

## Persistence

MVP: `ConcurrentDictionary<Guid, GameState>` in the API process. Restart clears all games.

## Determinism

`GameState` owns a seeded random sequence used for:

- Initial plot reserves
- Daily oil price deltas

Do not use wall-clock time or unseeded RNG for outcomes.

## API style

Simple controller or minimal-API endpoints calling a thin `GameSessionService` that loads state, calls `OilBaron.Game`, and saves. Prefer readable code over layers.
