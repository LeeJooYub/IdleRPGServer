# Repository Guidelines

## Project Structure & Module Organization
- Root: `GameAPIServer/`, `HiveServer/`, their `*.Tests/`, `ServerBin/`, and assets (`*.png`, `DB스키마.sql`).
- Game API: `GameAPIServer/Controllers/<Feature>/`, `Services/` (+ `Interfaces/`), `Repository/` (+ `Interfaces/`), `DTO/`, `Middleware/`, `Models/`.
- Hive API: `HiveServer/Controllers/`, `Repository/`, `Services/`, `Infrastructure/`, `Libraries/`.
- Config: `appsettings.json` and `appsettings.Development.json` per project (addresses, DB/Redis, `logdir`).
- Solutions: `GameAPIServer/GameAPIServer.sln`, `HiveServer/HiveServer.sln`.

## Build, Test, and Development Commands
- Restore/build (game): `dotnet restore && dotnet build GameAPIServer/GameAPIServer.sln -c Debug`.
- Restore/build (hive): `dotnet restore && dotnet build HiveServer/HiveServer.sln -c Debug`.
- Run (game): `dotnet run --project GameAPIServer/GameAPIServer.csproj` (binds to `ServerAddress`).
- Run (hive): `dotnet run --project HiveServer/HiveServer.csproj`.
- Tests: `dotnet test GameAPIServer.Tests` and `dotnet test HiveServer.Tests` (xUnit + coverlet).
- Local HTTP samples: use `.http` files under `GameAPIServer/HttpTest/`.

## Coding Style & Naming Conventions
- C#: 4 spaces; `PascalCase` for types/methods; `camelCase` for params/locals; interfaces prefixed `I`; private fields often `_field`.
- Controllers use attribute routing (e.g., `[HttpPost("login")]`) and live under feature folders.
- Prefer DI over `new`; register services in `Program.cs`.
- Logging: use `ILogger<T>`; ZLogger outputs JSON to `./log/` with daily rolling.
- Formatting: run `dotnet format` before pushes.

## Testing Guidelines
- Framework: xUnit; collectors via `coverlet.collector`.
- Naming: project suffix `*.Tests`; file example `UnitTest_Smoke.cs`; method style `Method_Scenario_Expected`.
- Run with coverage: `dotnet test -p:CollectCoverage=true`.

## Commit & Pull Request Guidelines
- Commits: Conventional style recommended: `feat:`, `fix:`, `docs:`, `refactor:`, optional scope (e.g., `feat(game/attendance): ...`).
- PRs: include purpose, affected endpoints, config changes, and sample requests/responses. Screenshots welcome for flows.
- Note: The README indicates PRs may not be actively accepted; open issues first if proposing changes.

## Security & Configuration Tips
- Do not commit secrets. Keep DB/Redis credentials and tokens in user-secrets or environment variables; override `appsettings.*` as needed.
- Verify `ServerAddress`, `DbConfig`, `HiveServerAddress`, and `logdir` before running; logs roll daily under each project's `log/`.
