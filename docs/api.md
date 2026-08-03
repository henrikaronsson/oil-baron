# API

Base URL (local): `http://localhost:5080`

JSON responses use camelCase property names.

## Health

### `GET /api/health`

Returns `{ "status": "ok" }`.

## Games

### `POST /api/games`

Body:

```json
{ "companyName": "Acme Oil", "seed": 12345 }
```

`seed` is optional; if omitted the server picks one and returns it in state.

Response `201`: game state DTO (includes `id`).

### `GET /api/games/{id}`

Response `200`: game state, or `404`.

### `POST /api/games/{id}/fields/{x}/{y}/buy`

### `POST /api/games/{id}/fields/{x}/{y}/drill`

### `POST /api/games/{id}/advance-month`

### `POST /api/games/{id}/sell-oil`

All mutation endpoints return `200` + updated state, or:

- `404` unknown game
- `400` `{ "error": "..." }` for illegal actions

## Game state DTO

```json
{
  "id": "uuid",
  "companyName": "Acme Oil",
  "seed": 12345,
  "month": 0,
  "calendarYear": 1972,
  "calendarMonth": 1,
  "calendarDay": 1,
  "cash": 100000,
  "oilBarrels": 0,
  "oilPrice": 50,
  "gridSize": 3,
  "oilFields": [
    {
      "x": 0,
      "y": 0,
      "owned": false,
      "drilled": false,
      "producing": false,
      "purchasePrice": 15000,
      "monthlyProduction": 10,
      "operatingCostPerMonth": 400,
      "estimatedReserves": 500,
      "remainingReserves": null
    }
  ]
}
```

`month` is the turn index from game start (`0` = first month). `calendarYear` / `calendarMonth` (1–12) / `calendarDay` are the in-world date (epoch **1 January, 1972**); the client formats these for the HUD (e.g. `Jan 1`, `Apr 1`).

`estimatedReserves` is always present. `remainingReserves` is `null` until the field is drilled; after drill it is the remaining barrels underground (0 for a dry hole).
