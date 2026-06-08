FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MediLabo.Patient.API/MediLabo.Patient.API.csproj", "MediLabo.Patient.API/"]
COPY ["MediLabo.Patient.Database/MediLabo.Patient.Database.csproj", "MediLabo.Patient.Database/"]
COPY ["MediLabo.Patient.Domain/MediLabo.Patient.Domain.csproj", "MediLabo.Patient.Domain/"]
RUN dotnet restore "MediLabo.Patient.API/MediLabo.Patient.API.csproj"
COPY . .
WORKDIR "/src/MediLabo.Patient.API"
RUN dotnet build "./MediLabo.Patient.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MediLabo.Patient.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediLabo.Patient.API.dll"]
