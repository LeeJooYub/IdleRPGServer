# Repository Guidelines

## Project Structure & Modules
- Target: .NET 9 Web API for game endpoints.
- Layout: `Controllers/<Feature>/`, `Services/` (+ `Interfaces/`), `Repository/` (+ `Interfaces/`), `DTO/`, `Middleware/`, `Models/`.
- Config: `appsettings.json` and `appsettings.Development.json` (addresses, DB/Redis, `logdir`).
- Build output: `ServerBin/GameServer/`.
- Notable: middlewares `VersionCheckMiddleware`, `CheckUserAuthMiddleware`, `LockRedisMiddleware`; master data via `IMasterDb.Load()` on startup.

## Build, Run, Dev
- Restore/build: `dotnet restore` && `dotnet build GameAPIServer/GameAPIServer.sln -c Debug`.
- Run: `dotnet run --project GameAPIServer/GameAPIServer.csproj` (binds to `ServerAddress`).
- HTTP smoke tests: use `GameAPIServer/HttpTest/*.http` in VS Code/JetBrains.
- Prereqs: MySQL (`game_db`, `master_db`), Redis at `localhost`; tweak in `DbConfig` section.

## Coding Style & Naming
- C#: 4 spaces; `PascalCase` types/methods; `camelCase` params/locals; interfaces start with `I`.
- Controllers: attribute routing (`[HttpPost("login")]`), one type per file under `Controllers/<Feature>/`.
- DI-first: register services in `Program.cs`; avoid manual instantiation.
- Logging: prefer injected `ILogger<T>`; ZLogger writes JSON files under `./log/` and console.

## Testing Guidelines
- Tests live in `GameAPIServer.Tests/` (xUnit) and reference this project.
- Run tests: `dotnet test GameAPIServer.Tests` or from repo root `dotnet test`.
- Keep `.http` samples minimal and up to date (e.g., `/login`, `/mail/getlist`, `/attendance/checkattendance`).

## Commits & PRs
- Conventional style observed: `feat:`, `fix:`, `docs:`, `refactor:`; optional scope like `feat(gameserver/attendance): ...`.
- PRs: include purpose, affected endpoints, sample `.http` calls, and any schema changes.

## Security & Config
- Do not commit real credentials. For shared dev, move secrets to user-secrets or env vars.
- Verify `ServerAddress`, `DbConfig`, and `logdir` before running; logs roll daily.
