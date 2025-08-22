FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["src/AdvertisingPlatforms.API/AdvertisingPlatforms.API.csproj", "AdvertisingPlatforms.API/"]
COPY ["src/AdvertisingPlatforms.Application/AdvertisingPlatforms.Application.csproj", "AdvertisingPlatforms.Application/"]
COPY ["src/AdvertisingPlatforms.Contracts/AdvertisingPlatforms.Contracts.csproj", "AdvertisingPlatforms.Contracts/"]
COPY ["src/AdvertisingPlatforms.Domain/AdvertisingPlatforms.Domain.csproj", "AdvertisingPlatforms.Domain/"]
COPY ["src/AdvertisingPlatforms.Infrastructure/AdvertisingPlatforms.Infrastructure.csproj", "AdvertisingPlatforms.Infrastructure/"]
RUN dotnet restore "./AdvertisingPlatforms.API/AdvertisingPlatforms.API.csproj"
COPY src/ .
WORKDIR "/src/AdvertisingPlatforms.API"
RUN dotnet build "AdvertisingPlatforms.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./AdvertisingPlatforms.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdvertisingPlatforms.API.dll"]