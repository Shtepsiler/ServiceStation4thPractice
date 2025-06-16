
# JOBS.API/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8083
ENV ASPNETCORE_URLS=http://+:8083

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files in dependency order
COPY ["JOBS.DAL/JOBS.DAL.csproj", "JOBS.DAL/"]
COPY ["JOBS.BLL/JOBS.BLL.csproj", "JOBS.BLL/"]
COPY ["JOBS.API/JOBS.API.csproj", "JOBS.API/"]

# Restore dependencies
RUN dotnet restore "JOBS.API/JOBS.API.csproj"

# Copy source code
COPY . .
WORKDIR "/src/JOBS.API"

# Build (removed dev-certs as they're not needed in containers)
RUN dotnet build "JOBS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JOBS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "JOBS.API.dll"]