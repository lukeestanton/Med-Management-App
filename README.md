# Med Management App

## Overview
This repository contains a collection of .NET 8 projects that work together to support a sample medical management platform. It includes an ASP.NET Core Web API for clinic operations, a cross-platform .NET MAUI client, shared domain libraries, and a simple console runner. The solution illustrates how to share business logic across multiple front ends while exposing it through HTTP services.

## Repository structure

| Project | Description |
| --- | --- |
| `Api.Clinic/` | ASP.NET Core Web API that surfaces clinic functionality, integrates with PostgreSQL via Npgsql, and exposes Swagger/OpenAPI metadata. |
| `MedManagementLibrary/` | Shared domain and service logic for appointments, patients, physicians, insurance, and treatments. Reused by the API, console app, and MAUI client. |
| `MedManagementProject/` | Console application demonstrating the domain library without the web or UI layers. |
| `MauiApp1/` | .NET MAUI client (targeting .NET 8 Mac Catalyst by default) showcasing a cross-platform front-end that can consume the shared library. |
| `csharp.sln` | Solution file that combines the projects for convenient restoring, building, and IDE support. |

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- (Optional) A PostgreSQL instance if you plan to run the API against a real database. Update `Api.Clinic/appsettings.json` with your connection string as needed.
- (Optional) Platform-specific tooling if you intend to deploy the MAUI client to Windows, macOS, Android, or iOS.

## Getting started
1. Restore dependencies:
   ```bash
   dotnet restore csharp.sln
   ```
2. Build the entire solution:
   ```bash
   dotnet build csharp.sln
   ```

### Running the Web API
```bash
dotnet run --project Api.Clinic/Api.Clinic.csproj
```
The API will launch on the configured Kestrel ports and expose interactive documentation at `/swagger` when running in development.

### Running the console sample
```bash
dotnet run --project MedManagementProject/MedManagementProject.csproj
```

### Running the MAUI client
The MAUI project targets Mac Catalyst by default. To run it locally, specify the target framework that matches your environment, for example:
```bash
dotnet build MauiApp1/MauiApp1.csproj -f net8.0-maccatalyst
```
Refer to the [official MAUI documentation](https://learn.microsoft.com/dotnet/maui/) for additional setup steps and platform-specific deployment instructions.

## Contributing
1. Create a new branch for your work.
2. Make your changes and add accompanying tests when applicable.
3. Ensure the solution builds successfully before opening a pull request.
4. Submit a PR detailing the motivation and implementation of your changes.

## License
This project does not currently specify a license. Please contact the repository owner for usage guidelines.
