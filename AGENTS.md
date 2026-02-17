# Agent Guide for sw-sdk-dotnet

This repository contains a .NET Framework SDK and MSTest-based test suites. Use the guidance below when acting as an automated coding agent.

## Repository Overview
- Solution: `SW-sdk.sln`
- SDK projects: `SW-sdk`, `SW-sdk-45`, `SAT.Services`
- Test projects: `Test_SW-sdk` (.NET 4.0) and `Test_SW-sdk-45` (.NET 4.5)
- Example app: `Example/Example.sln`

## Build, Test, and Lint Commands

### Prereqs
- Windows build environment with MSBuild and VSTest (Visual Studio Build Tools or full VS).
- NuGet CLI for `packages.config` restore.

### Restore
- Restore solution packages:
  - `nuget restore SW-sdk.sln`

### Build
- Build Debug (solution):
  - `msbuild SW-sdk.sln /t:Build /p:Configuration=Debug`
- Build Release (solution):
  - `msbuild SW-sdk.sln /t:Build /p:Configuration=Release`
- Build a single project (example):
  - `msbuild SW-sdk\SW-sdk.csproj /t:Build /p:Configuration=Release`

### Tests (MSTest)
Tests are classic MSTest unit tests compiled to DLLs. Use `vstest.console.exe`.

- Build tests before running:
  - `msbuild Test_SW-sdk\Test_SW-sdk.csproj /t:Build /p:Configuration=Debug`
  - `msbuild Test_SW-sdk-45\Test_SW-sdk-45.csproj /t:Build /p:Configuration=Debug`

- Run all tests in a test DLL:
  - `vstest.console.exe Test_SW-sdk\bin\Debug\Test_SW-sdk.dll`
  - `vstest.console.exe Test_SW-sdk-45\bin\Debug\Test_SW-sdk-45.dll`

- Run a single test by name (recommended):
  - `vstest.console.exe Test_SW-sdk\bin\Debug\Test_SW-sdk.dll /Tests:Test_SW.Services.Authentication_Test.Authentication_Test.ValidateAuthenticationV2`

- Run a subset using test case filter:
  - `vstest.console.exe Test_SW-sdk\bin\Debug\Test_SW-sdk.dll /TestCaseFilter:"FullyQualifiedName~Authentication_Test"`

### Where to find vstest.console.exe
Typical locations (choose the installed one):
- `C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\Extensions\TestPlatform\vstest.console.exe`
- `C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\Extensions\TestPlatform\vstest.console.exe`

### Lint
- No linting or formatting tools are configured in this repo. Use the existing code style.

## Coding Style and Conventions

### Language and Framework
- C# targeting .NET Framework (4.0 and 4.5).
- Uses MSTest for unit tests (`[TestClass]`, `[TestMethod]`).

### Formatting
- 4 spaces indentation, no tabs.
- Braces on their own line (Allman style).
- One namespace per file, namespace and type braces aligned.
- Keep files short and focused; small classes often wrap base services.

### File and Type Organization
- Namespaces reflect folder structure, e.g. `SW.Services.Stamp`.
- Base classes in `Base*` files (e.g. `BaseStamp`, `BaseResend`).
- Response/Request DTOs live alongside their service.

### Naming
- Public classes, methods, properties: `PascalCase`.
- Private fields: `_camelCase` (e.g. `_handler`, `_url`).
- Parameters and locals: `camelCase`.
- Test classes and methods: `PascalCase` with suffix `_Test` (e.g. `Authentication_Test`).

### Imports
- `using` directives at the top.
- Typically ordered: `System` namespaces first, then third-party, then solution namespaces.
- Separate logical groups with a blank line when mixing `System` and project namespaces.

### Types and Declarations
- Prefer explicit types for new service instances:
  - `Authentication auth = new Authentication(...);`
- `var` is used for responses and intermediate values when the type is obvious.
- DTOs may use `[DataContract]` and `[DataMember]` for JSON payloads.

### Error Handling
- Service methods wrap request logic in `try/catch` and delegate to a response handler.
- Exceptions are converted into response objects (see `ResponseHandler<T>` pattern).
- Validate request headers before issuing HTTP requests (see `Validation.ValidateHeaderParameters`).

### HTTP and Serialization
- HTTP calls use `HttpWebRequest` and `HttpWebResponse`.
- JSON (de)serialization via `Newtonsoft.Json.JsonConvert`.
- `ResponseHandler<T>` returns `status`, `message`, `messageDetail` on failures.

### Optional Parameters and Defaults
- Optional parameters often default to `null` or `0` (e.g. proxy info, `folioSustitucion`).
- Keep signatures consistent with existing overloads for token vs user/password auth.

### Tests
- MSTest tests call SDK services with `BuildSettings` from test helpers.
- Assertions use `Assert.IsTrue` and `Assert.AreEqual`.
- Tests are organized by service area in `Test_SW-sdk/Services/...`.

## Repository-Specific Notes
- Package references are managed with `packages.config` (NuGet restore required).
- Two framework targets exist (4.0 and 4.5) with similar test suites.
- Use `RequestHelper.NormalizeBaseUrl` where URLs are accepted.

## Cursor/Copilot Rules
- No Cursor rules found in `.cursor/rules/` or `.cursorrules`.
- No GitHub Copilot instructions found in `.github/copilot-instructions.md`.

## Adding New Code
- Mirror the existing service structure: base service + response handler + response DTO.
- Keep public surface minimal; internal request helpers live in service classes.
- When extending a service, include both token and user/password constructors.

## Example Paths to Reference
- Core services: `SW-sdk\Services\Stamp\Stamp.cs`
- Response handler pattern: `SW-sdk\Services\ResponseHandler.cs`
- Test style: `Test_SW-sdk\Services\Authentication\Authentication_Test.cs`
