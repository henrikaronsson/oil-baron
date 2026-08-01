# Economy

All values are authoritative in `OilBaron.Game` (`EconomyConstants`). Keep this file in sync when changing numbers.

## Starting state

| Constant | Value |
|----------|------:|
| Starting cash | 100_000 |
| Starting day | 0 |
| Starting oil inventory (barrels) | 0 |
| Starting oil price ($ / barrel) | 50 |

## Costs

| Action | Cost |
|--------|-----:|
| Buy plot | 15_000 |
| Drill plot | 25_000 |

## Production

When a day advances, each **producing** well:

1. Computes daily output: `max(1, remainingReserve / 100)` (integer division).
2. Produces `min(output, remainingReserve)` barrels into inventory.
3. Decreases remaining reserve by that amount.
4. If remaining reserve hits 0, the well stops producing (still drilled).

## Oil price

On each day advance, after production:

- Price performs a seeded random walk: `delta` in `[-3, +3]` inclusive.
- New price is clamped to `[20, 100]`.
- Sell oil uses the **current** price (after advances that already happened).

## Determinism

Reserves and price walks use the game’s seeded PRNG only. Same seed + same command sequence ⇒ same outcomes.
