# AGENTS.md — oil-baron-web

Angular presentation client for Oil Baron.

## Rules

- Standalone components, Bootstrap 5, SCSS.
- **No business rules** here — no prices, production math, or action validity beyond UI affordances from server state / API errors.
- Use `GameApiService` for all game mutations.
- Keep components lean; put HTTP in services.
- Prefer signals for local UI state.

## Local run

API must be running at `http://localhost:5080`.
