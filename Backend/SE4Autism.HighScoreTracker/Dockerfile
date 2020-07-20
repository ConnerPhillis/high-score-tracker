#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SE4Autism.HighScoreTracker/SE4Autism.HighScoreTracker.csproj", "SE4Autism.HighScoreTracker/"]
RUN dotnet restore "SE4Autism.HighScoreTracker/SE4Autism.HighScoreTracker.csproj"
COPY . .
WORKDIR "/src/SE4Autism.HighScoreTracker"
RUN dotnet build "SE4Autism.HighScoreTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SE4Autism.HighScoreTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE4Autism.HighScoreTracker.dll"]