
# PARTS.API/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8083
ENV ASPNETCORE_URLS=http://+:8083

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files in dependency order
COPY ["PARTS.DAL/PARTS.DAL.csproj", "PARTS.DAL/"]
COPY ["PARTS.BLL/PARTS.BLL.csproj", "PARTS.BLL/"]
COPY ["PARTS.API/PARTS.API.csproj", "PARTS.API/"]
COPY ["ServiceCenterPayment/ServiceCenterPayment.csproj", "ServiceCenterPayment/"]

# Restore dependencies
RUN dotnet restore "PARTS.API/PARTS.API.csproj"

# Copy source code and models.json
COPY . .
WORKDIR "/src/PARTS.API"

# Build (removed dev-certs as they're not needed in containers)
RUN dotnet build "PARTS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PARTS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy models.json to the working directory
COPY ["PARTS.API/models.json", "./models.json"]

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "PARTS.API.dll"]