<p align="center">
  <img src="docs/icons/logo.png" alt="Oil Baron" width="322" height="81" />
</p>

# Oil Baron

Open-source, browser-based oil company management simulation. Inspired by classic business strategy games, with original mechanics, assets, and code.

## Stack

- **Frontend:** Angular 20, standalone components, Tailwind CSS 4, SCSS (components) + global CSS theme
- **Backend:** .NET 10 Web API
- **Game rules:** `OilBaron.Game` (pure C# library; deterministic, seeded)
- **Persistence (MVP):** in-memory only, for now ...
z§
## Repository layout

```text
docs/                     Design and architecture docs
src/oil-baron-web/        Angular client (presentation only)
src/OilBaron.Api/         HTTP API + in-memory game store
src/OilBaron.Game/        Deterministic simulation (business rules)
tests/OilBaron.Game.Tests/
```

## Prerequisites

- .NET 10 SDK
- Node.js 22+ and npm
- Angular CLI 20 (optional; `npx` works)

## Run locally

### API

```powershell
cd src/OilBaron.Api
dotnet run
```

API listens on `http://localhost:5080` (see `launchSettings.json`).

### Web client

```powershell
cd src/oil-baron-web
npm install
npm start
```

Open `http://localhost:4200`.

### Tests

```powershell
dotnet test OilBaron.slnx
```

## Documentation

| Doc | Topic |
|-----|--------|
| [docs/vision.md](docs/vision.md) | Product vision |
| [docs/game-design.md](docs/game-design.md) | Gameplay loop |
| [docs/economy.md](docs/economy.md) | Economy constants |
| [docs/architecture.md](docs/architecture.md) | Technical architecture |
| [docs/api.md](docs/api.md) | HTTP API |
| [docs/tailwind.md](docs/tailwind.md) | Tailwind + 1970s design system |
| [AGENTS.md](AGENTS.md) | Conventions for coding agents |

## License

MIT — see [LICENSE](LICENSE).
