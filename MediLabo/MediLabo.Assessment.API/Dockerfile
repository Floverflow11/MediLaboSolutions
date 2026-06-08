FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MediLabo.Assessment.API/MediLabo.Assessment.API.csproj", "MediLabo.Assessment.API/"]
RUN dotnet restore "MediLabo.Assessment.API/MediLabo.Assessment.API.csproj"
COPY . .
WORKDIR "/src/MediLabo.Assessment.API"
RUN dotnet build "./MediLabo.Assessment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MediLabo.Assessment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediLabo.Assessment.API.dll"]
