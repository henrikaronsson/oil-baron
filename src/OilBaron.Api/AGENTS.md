# AGENTS.md — OilBaron.Api

Thin HTTP host over `OilBaron.Game`.

## Rules

- Do not implement economy or validity rules here — call `OilGame`.
- Keep controllers thin; session logic in `GameSessionService`.
- In-memory store only unless docs/roadmap say otherwise.
- Use Newtonsoft.Json (camelCase).
- CORS allows `http://localhost:4200` for local Angular.

## Endpoints

See `docs/api.md`.
