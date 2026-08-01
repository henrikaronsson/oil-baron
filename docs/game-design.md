# Game design

## MVP loop (current)

1. Start a new game (company name + optional seed).
2. Inspect a fixed **3×3** land grid.
3. **Buy** an unowned plot (lease).
4. **Drill** an owned plot.
5. **Advance day** — producing wells yield barrels; market price moves.
6. **Sell oil** — convert inventory to cash at the current price.
7. Repeat until cash strategy and depletion matter.

## Map

- Grid size: 3×3 (`x` and `y` in `0..2`).
- Each plot has a hidden oil **reserve** generated from the game seed.
- After drilling:
  - Reserve &gt; 0 → well is **producing**.
  - Reserve = 0 → dry hole (drilled, not producing).

Client may show ownership and drill status. Exact remaining reserve is server-known; MVP can expose remaining reserve after drill for clarity/learning (documented in API).

## Actions

| Action | Requirements |
|--------|----------------|
| Buy plot | Unowned, enough cash |
| Drill plot | Owned, not yet drilled, enough cash |
| Advance day | Always (while game exists) |
| Sell oil | Inventory &gt; 0 (selling 0 is a no-op success) |

Illegal actions return an error; state is unchanged.

## Win / lose

Not enforced in MVP. Informal goal: grow cash by producing and selling smarter than you spend on leases and drills.

## Future (not MVP)

- Survey / fog of war
- Operating costs, equipment tiers
- Pipelines / storage limits
- AI rivals / multiplayer
- Larger maps, terrain
