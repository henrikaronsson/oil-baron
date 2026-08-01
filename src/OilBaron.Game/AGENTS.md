# AGENTS.md — OilBaron.Game

Pure deterministic simulation library. **All business rules live here.**

## Rules

- No ASP.NET, HTTP, or I/O dependencies.
- Use the state's seeded `Random` for any game outcome randomness.
- Keep `EconomyConstants` aligned with `docs/economy.md`.
- Commands return `GameCommandResult`; on failure, leave state unchanged.
- Prefer clear static methods on `OilGame` over deep hierarchies.

## Tests

Add/extend cases in `tests/OilBaron.Game.Tests` for every new rule.
