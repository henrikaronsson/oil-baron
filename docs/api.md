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

### `POST /api/games/{id}/plots/{x}/{y}/buy`

### `POST /api/games/{id}/plots/{x}/{y}/drill`

### `POST /api/games/{id}/advance-day`

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
  "day": 0,
  "cash": 100000,
  "oilBarrels": 0,
  "oilPrice": 50,
  "gridSize": 3,
  "plots": [
    {
      "x": 0,
      "y": 0,
      "owned": false,
      "drilled": false,
      "producing": false,
      "remainingReserve": null
    }
  ]
}
```

`remainingReserve` is `null` until the plot is drilled; after drill it is the remaining barrels underground (0 for a dry hole).
