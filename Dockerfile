# syntax = docker/dockerfile:1.2

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Define build arguments
ARG DATABASE_SERVER
ARG DATABASE_PORT
ARG DATABASE_NAME
ARG DATABASE_USER
ARG DATABASE_PASSWORD

# Set environment variables based on build arguments
ENV DATABASE_CONNECTION_STRING="server=$DATABASE_SERVER;port=$DATABASE_PORT;database=$DATABASE_NAME;user=$DATABASE_USER;password=$DATABASE_PASSWORD"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SuperHeroAPI.csproj", "."]
RUN dotnet restore "./SuperHeroAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./SuperHeroAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SuperHeroAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperHeroAPI.dll"]