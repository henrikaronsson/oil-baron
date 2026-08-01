# AGENTS.md — Oil Baron

Instructions for AI coding agents working in this repository.

## Before changing code

1. Read [README.md](README.md).
2. Read this file.
3. Read all docs in [docs/](docs/).
4. Read the nested `AGENTS.md` inside the project you are modifying.

Follow every documented convention.

## General principles

- Build incrementally.
- Keep the game playable at every stage.
- Prefer readable code over clever code.
- Use pragmatic architecture. Do not over-engineer.
- Keep the simulation **deterministic**.
- The **server owns all business rules**.
- Angular is **only** responsible for presentation.
- Never duplicate business rules between client and server.
- Every feature should include appropriate tests.
- Compile and run tests before finishing.
- Update documentation whenever behaviour changes.

## Architecture rules

- Business rules live in `src/OilBaron.Game` only.
- `OilBaron.Api` maps HTTP to game commands and stores sessions; it must not invent economy rules.
- `oil-baron-web` sends intents and renders state; it must not compute prices, production, or validity beyond disabling controls from server state / errors.
- Prefer simple controllers/endpoints + services. No Clean Architecture ceremony.
- Serialization: **Newtonsoft.Json**.

## Simulation rules

- Given the same seed and the same sequence of commands, results must match.
- Use a seeded PRNG owned by the game state; do not use `Random.Shared` or wall-clock randomness for game outcomes.
- Economy constants belong in one place in `OilBaron.Game` and must stay aligned with [docs/economy.md](docs/economy.md).

## Frontend rules (Angular)

- Standalone components (no NgModules unless required).
- Bootstrap 5 + SCSS.
- Services for HTTP; lean components.
- Prefer signals and/or RxJS where natural; no global state libraries unless requested.

## Testing

- Unit-test simulation rules in `tests/OilBaron.Game.Tests`.
- Cover determinism, legal/illegal actions, and economy effects for each new rule.

## When finishing a task

Summarize:

- what you changed
- why
- trade-offs
- possible future improvements
