# syntax = docker/dockerfile:1.2

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SuperHeroAPI.csproj", "."]
RUN dotnet restore "./SuperHeroAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN --mount=type=secret,id=_env,dst=/run/secrets/.env \
    dotnet build "./SuperHeroAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SuperHeroAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperHeroAPI.dll"]