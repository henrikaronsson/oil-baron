# Economy

All values are authoritative in `OilBaron.Game` (`EconomyConstants`). Keep this file in sync when changing numbers.

## Starting state

| Constant | Value |
|----------|------:|
| Starting cash | 100_000 |
| Starting month (turn index) | 0 |
| Starting calendar | 1 January, 1972 |
| Starting oil inventory (barrels) | 0 |
| Starting oil price ($ / barrel) | 50 |

Turn index `0` is **January 1972** (before the October 1973 oil crisis). Each advance increases the turn index by 1 and moves the in-world calendar forward one month. The API exposes both `month` (turn index) and `calendarYear` / `calendarMonth` / `calendarDay` for display.

## Costs

| Action / field default | Cost |
|------------------------|-----:|
| Buy oil field (`DefaultPurchasePrice`) | 15_000 |
| Drill oil field | 25_000 |
| Operating cost per producing field / month | 400 |

## Production

Each oil field stores:

| Property | Default |
|----------|--------:|
| `MonthlyProduction` | 10 barrels / month |
| `EstimatedReserves` | seeded at create (0…2000 step 100) |
| `RemainingReserves` | starts equal to estimated; depletes on produce |

When a **month** advances, each **producing** field (`Drilled && RemainingReserves > 0`):

1. Deducts `OperatingCostPerMonth` from cash (cash may go negative).
2. Produces `min(MonthlyProduction, RemainingReserves)` barrels into inventory.
3. Decreases remaining reserves by that amount.
4. If remaining reserves hit 0, the field stops producing (still drilled).

## Oil price

On each month advance, after production:

- Price performs a seeded random walk: `delta` in `[-3, +3]` inclusive.
- New price is clamped to `[20, 100]`.
- Sell oil uses the **current** price (after advances that already happened).

## Determinism

Reserves and price walks use the game’s seeded PRNG only. Same seed + same command sequence ⇒ same outcomes.
