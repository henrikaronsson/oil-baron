# AGENTS.md — oil-baron-web

Angular presentation client for Oil Baron.

## Rules

- Standalone components, Tailwind CSS 4, component SCSS where needed.
- **No business rules** here — no prices, production math, or action validity beyond UI affordances from server state / API errors.
- Use `GameApiService` for all game mutations.
- Keep components lean; put HTTP in services.
- Prefer signals for local UI state.
- Use semantic `--ob-*` tokens and shared `ob-*` UI components under `src/app/ui/`.
- Do not construct Tailwind class names dynamically; use typed variant maps.
- See [docs/tailwind.md](../../docs/tailwind.md) for the design system.

## Local run

API must be running at `http://localhost:5080`.
