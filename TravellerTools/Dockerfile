# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first to maximize layer reuse during restore.
COPY ["TravellerTools.sln", "./"]
COPY ["Grauenwolf.TravellerTools.Animals/Grauenwolf.TravellerTools.Animals.csproj", "Grauenwolf.TravellerTools.Animals/"]
COPY ["Grauenwolf.TravellerTools.Characters/Grauenwolf.TravellerTools.Characters.csproj", "Grauenwolf.TravellerTools.Characters/"]
COPY ["Grauenwolf.TravellerTools.Encounters/Grauenwolf.TravellerTools.Encounters.csproj", "Grauenwolf.TravellerTools.Encounters/"]
COPY ["Grauenwolf.TravellerTools.Equipment/Grauenwolf.TravellerTools.Equipment.csproj", "Grauenwolf.TravellerTools.Equipment/"]
COPY ["Grauenwolf.TravellerTools.Services/Grauenwolf.TravellerTools.Services.csproj", "Grauenwolf.TravellerTools.Services/"]
COPY ["Grauenwolf.TravellerTools.Shared/Grauenwolf.TravellerTools.Shared.csproj", "Grauenwolf.TravellerTools.Shared/"]
COPY ["Grauenwolf.TravellerTools.TradeCalculator/Grauenwolf.TravellerTools.TradeCalculator.csproj", "Grauenwolf.TravellerTools.TradeCalculator/"]
COPY ["Grauenwolf.TravellerTools.Tests/Grauenwolf.TravellerTools.Tests.csproj", "Grauenwolf.TravellerTools.Tests/"]
COPY ["Grauenwolf.TravellerTools.Web/Grauenwolf.TravellerTools.Web.csproj", "Grauenwolf.TravellerTools.Web/"]
COPY ["Tortuga.Yardarm/Tortuga.Yardarm.csproj", "Tortuga.Yardarm/"]

RUN dotnet restore "TravellerTools.sln"

COPY . .
RUN dotnet publish "Grauenwolf.TravellerTools.Web/Grauenwolf.TravellerTools.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production \
	ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Grauenwolf.TravellerTools.Web.dll"]