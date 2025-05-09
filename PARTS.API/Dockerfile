#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PARTS.API/PARTS.API.csproj", "PARTS.API/"]
COPY ["PARTS.BLL/PARTS.BLL.csproj", "PARTS.BLL/"]
COPY ["PARTS.DAL/PARTS.DAL.csproj", "PARTS.DAL/"]
RUN dotnet restore "./PARTS.API/./PARTS.API.csproj"
COPY . .
WORKDIR "/src/PARTS.API"
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
RUN dotnet build "./PARTS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PARTS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PARTS.API.dll"]