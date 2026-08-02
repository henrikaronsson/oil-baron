# Tailwind CSS — Oil Baron web client

Oil Baron’s Angular client uses **Tailwind CSS v4** as the primary styling system, with a light early-1970s corporate design language.

## Why Tailwind instead of Bootstrap

Bootstrap fitted the MVP, but the UI now needs:

- A period-specific light theme (warm paper, petrol blue, restrained geometry)
- Semantic design tokens for game concepts (cash, oil, production, market)
- Reusable Angular components with typed variants
- Utility-first layout without Bootstrap’s opinionated component CSS

Tailwind keeps presentation in templates while tokens stay in one theme layer. Game rules remain server-side; the client only styles state the API returns.

## Versions

| Package | Role | Version (approx.) |
|---------|------|-------------------|
| Angular | App framework | 20 |
| `tailwindcss` | Utility engine | ^4.3.3 |
| `@tailwindcss/postcss` | Angular build integration | ^4.3.3 |
| `postcss` | PostCSS host | ^8.5 |

`ng add tailwindcss` was attempted; the published `tailwindcss` package does not ship Angular schematics, so setup was completed manually (same end state as the official Angular guide).

## Installation

From `src/oil-baron-web`:

```bash
npm install -D tailwindcss @tailwindcss/postcss postcss
```

## PostCSS configuration

File: `.postcssrc.json` (UTF-8 **without BOM**):

```json
{
  "plugins": {
    "@tailwindcss/postcss": {}
  }
}
```

Do **not** add a legacy `tailwind.config.js` for a fresh Tailwind v4 project. Theme tokens live in CSS.

## Where Tailwind is imported

Global entry: `src/styles.css` (referenced from `angular.json`):

```css
@import 'tailwindcss';
@import './styles/theme.css';
@import './styles/typography.css';
@import './styles/textures.css';
@import './styles/utilities.css';
```

Component SCSS remains allowed for game-specific surfaces (for example the land grid in `game-page.scss`).

## Design tokens (light theme)

Semantic tokens are defined in `src/styles/theme.css` as CSS custom properties (`--ob-*`), then exposed to Tailwind via `@theme inline` where useful (`font-display`, `font-sans`, `font-mono`, and selected `ob-*` colour aliases).

Examples:

| Token | Role |
|-------|------|
| `--ob-page-bg` | Warm office / planning-board background |
| `--ob-surface-paper` | Raised corporate panels |
| `--ob-surface-document` | Contracts / start-game forms |
| `--ob-surface-terminal` | Contextual dark readouts |
| `--ob-action-primary` | Petrol-blue primary actions |
| `--ob-action-financial` | Olive financial confirmation |
| `--ob-indicator-cash` / `--ob-indicator-oil` | Resource meaning |
| `--ob-focus` | Keyboard focus ring |

Prefer tokens over raw hex in templates:

```html
<div class="border border-[var(--ob-border)] bg-[var(--ob-surface-paper)]">
```

### Light theme vs contextual dark surfaces

The default theme is light (cream / ivory / warm grey). Dark surfaces are intentional and limited:

- `ob-panel` variant `terminal`
- Trading tickers / instrument panels (future)
- Emergency or night-ops screens (future)

Do not convert the whole app into a dark dashboard.

## Typography

Loaded in `index.html` (Google Fonts):

- **Display:** Barlow Condensed — mastheads and section titles
- **Body:** Source Sans 3 — readable UI copy
- **Mono:** IBM Plex Mono — cash, prices, seeds, production figures

Helpers in `typography.css`: `.ob-label`, `.ob-mono`, `.ob-tabular`, `.ob-section-title`.

## How to use Tailwind in templates

```html
<div class="grid grid-cols-1 gap-3 sm:grid-cols-2 lg:grid-cols-5">
  <ob-stat-card label="Cash" tone="cash">
    <ob-money-value [amount]="state.cash" label="Cash" />
  </ob-stat-card>
</div>
```

### Responsive prefixes

Desktop-first, still usable on smaller screens:

- `sm:` tablets / narrow laptops
- `lg:` / `xl:` wide HQ layouts

Example from the game page: plot rows stack to one column below `sm`, then a 3-column board.

### Hover, focus, disabled

Shared controls encode these states. Pattern for custom buttons:

```html
<button
  type="button"
  class="border border-[var(--ob-action-primary-border)]
         bg-[var(--ob-action-primary)]
         px-4 py-2 font-semibold
         text-[var(--ob-action-primary-text)]
         transition
         hover:bg-[var(--ob-action-primary-hover)]
         focus-visible:outline focus-visible:outline-2
         focus-visible:outline-offset-2
         focus-visible:outline-[var(--ob-focus)]
         disabled:cursor-not-allowed disabled:opacity-50"
>
  Purchase field
</button>
```

Prefer `<ob-button>` instead of repeating this.

## Reusable UI components

Located under `src/app/ui/`:

| Component | Purpose |
|-----------|---------|
| `ob-button` | Typed action variants |
| `ob-panel` | Corporate / document / terminal / control surfaces |
| `ob-alert` | Status messages (not colour-only: includes a label) |
| `ob-stat-card` | Resource / HUD metrics |
| `ob-form-field` | Label + control + hint |
| `ob-money-value` | Currency with tabular mono |
| `ob-badge` | Compact status marks |
| `ob-status-lamp` | Industrial indicator lamp |
| `ob-toolbar` | Action strip |

Import from `src/app/ui` (see `index.ts`).

### Button variants

```typescript
export type ObButtonVariant =
  | 'primary'
  | 'secondary'
  | 'financial'
  | 'warning'
  | 'danger'
  | 'quiet'
  | 'terminal';
```

Each variant maps to **complete** Tailwind class strings in TypeScript — never build class names with string concatenation.

### When to add a reusable component

Create one when the concept repeats and has meaning in the game UI (panels, money, status lamps). Do **not** wrap a one-off layout in a component only to hide Tailwind classes.

### When custom CSS is appropriate

- Land / map / plot board visuals (`game-page.scss`)
- Paper grain, tech grids, terminal scan lines (`textures.css`)
- Focus / inset utilities that are awkward as long utility strings
- Future canvas/SVG map styling

Do not write custom CSS that only duplicates `flex gap-2`.

## Adding a semantic colour token

1. Add `--ob-…` under `:root` in `theme.css`.
2. Optionally map it in `@theme inline` if you want a Tailwind colour utility.
3. Use `var(--ob-…)` in components or templates.
4. Mention the token in this doc if it becomes part of the public design system.

## Adding a component variant

1. Extend the typed union (for example `ObButtonVariant`).
2. Add a full class string to the variant map.
3. Use the variant from templates — keep raw `class` for layout only when needed.

## Dynamic values

Avoid:

```typescript
const className = `bg-${colour}-700`; // Tailwind may not detect this
```

Prefer explicit maps:

```typescript
const marketTrendClasses = {
  rising: 'text-[var(--ob-money-positive)]',
  stable: 'text-[var(--ob-text-secondary)]',
  falling: 'text-[var(--ob-money-negative)]',
} as const;
```

Use CSS custom properties for truly dynamic values (gauge rotation, progress width, company colours).

## Bootstrap removal

Removed:

- npm package `bootstrap` (^5.3.8)
- `@use 'bootstrap/scss/bootstrap'` from global styles
- Bootstrap utility / component classes from `game-page.html`

There was **no** Bootstrap JavaScript, Popper, or Bootstrap Icons dependency.

### Verify no Bootstrap remains

From the repo root:

```powershell
rg -i "bootstrap|btn-|form-control|data-bs-|alert-danger|navbar-|card-body" src/oil-baron-web --glob '!package-lock.json'
```

Expect only Angular’s `bootstrapApplication` in `main.ts`.

## Practical examples from this codebase

### Resource bar

See `game-page.html` — `ob-stat-card` grid with cash (`ob-money-value`), barrels, oil price, and producing count.

### Document panel (start game)

`ob-panel variant="document"` wraps company name / seed fields and a financial confirmation button.

### Explicit status mapping

`plotBadgeVariant()` / `plotBadgeLabel()` in `game-page.ts` map plot state to `ObBadgeVariant` without dynamic Tailwind class construction.

## Validation checklist

```powershell
cd src/oil-baron-web
npm install
npm run build
```

Confirm production CSS includes Tailwind layers (`@layer theme,base,components,utilities`) and design tokens such as `--ob-page-bg`.
