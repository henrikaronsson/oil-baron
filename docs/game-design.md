# Game design

## Setting

The campaign opens in **January 1972**, in the calm before the first oil crisis of October 1973. Tone, technology, and presentation lean early-1970s corporate (see [tailwind.md](tailwind.md)).

## MVP loop (current)

1. Start a new game (company name + optional seed).
2. Inspect a fixed **3×3** land grid of oil fields.
3. **Buy** an unowned oil field (lease).
4. **Drill** an owned oil field.
5. **Advance month** — producing fields yield barrels and pay operating cost; market price moves.
6. **Sell oil** — convert inventory to cash at the current price.
7. Repeat until cash strategy and depletion matter.

## Map

- Grid size: 3×3 (`x` and `y` in `0..2`).
- Each oil field has seeded **estimated** and **remaining** reserves (same value at create).
- Client always sees estimated reserves; remaining reserves are exposed after drill.
- After drilling:
  - Remaining reserves &gt; 0 → field is **producing**.
  - Remaining reserves = 0 → dry hole (drilled, not producing).

## Actions

| Action | Requirements |
|--------|----------------|
| Buy oil field | Unowned, enough cash for that field’s purchase price |
| Drill oil field | Owned, not yet drilled, enough cash |
| Advance month | Always (while game exists) |
| Sell oil | Inventory &gt; 0 (selling 0 is a no-op success) |

Illegal actions return an error; state is unchanged.

## Win / lose

Not enforced in MVP. Informal goal: grow cash by producing and selling smarter than you spend on leases, drills, and monthly operating costs. Cash may go negative from operating costs.

## Future (not MVP)

- Survey noise / fog of war on estimates
- Per-field randomized purchase / production
- Equipment tiers
- Pipelines / storage limits
- AI rivals / multiplayer
- Larger maps, terrain
- Bankruptcy when cash stays negative
