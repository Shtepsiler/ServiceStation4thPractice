
# IDENTITY.API/Dockerfile  
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8082
ENV ASPNETCORE_URLS=http://+:8082

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files in dependency order
COPY ["IDENTITY.DAL/IDENTITY.DAL.csproj", "IDENTITY.DAL/"]
COPY ["IDENTITY.BLL/IDENTITY.BLL.csproj", "IDENTITY.BLL/"]  
COPY ["IDENTITY.API/IDENTITY.API.csproj", "IDENTITY.API/"]

# Restore dependencies
RUN dotnet restore "IDENTITY.API/IDENTITY.API.csproj"

# Copy source code
COPY . .
WORKDIR "/src/IDENTITY.API"

# Build
RUN dotnet build "IDENTITY.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IDENTITY.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "IDENTITY.API.dll"]