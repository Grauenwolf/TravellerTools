# Traveller Tools II
Tools for the Traveller RPG

# Build and run
## Set up local build environment
1. Install the .NET 10 SDK: https://learn.microsoft.com/dotnet/core/install/
2. Run `dotnet --info` to confirm the SDK is available.

## Build locally
1. Open a console and go to `TravellerTools/TravellerTools/`.
2. Run `dotnet restore TravellerTools.sln`.
3. Run `dotnet build TravellerTools.sln`.
4. Run `dotnet test Grauenwolf.TravellerTools.Tests/Grauenwolf.TravellerTools.Tests.csproj`.
5. Run `dotnet run --project Grauenwolf.TravellerTools.Web/Grauenwolf.TravellerTools.Web.csproj`.
6. Open `http://localhost:5000/` or the URL shown in the app output.

## Docker (production-like)
- Open a console and go to `TravellerTools/`
- `docker compose build`
- `docker compose up -d travellertools-web`
- Now open a browser and head to http://localhost:8080/
- To stop: `docker compose down`

## Docker (development profile)
- Open a console and go to `TravellerTools/`
- `docker compose --profile dev up --build travellertools-web-dev`
- Now open a browser and head to http://localhost:8081/
- This profile mounts the source tree and runs the app with `dotnet run` inside an SDK container.
- To stop: `docker compose --profile dev down`

# Issues
If Docker runtime behavior differs from local `dotnet run`, please open an issue with:
- the exact command used (`docker build`, `docker compose up`, profile)
- container logs
- browser/network errors for static files (if styles/scripts are missing)
