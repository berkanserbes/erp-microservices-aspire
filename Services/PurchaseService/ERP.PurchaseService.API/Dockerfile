# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/PurchaseService/ERP.PurchaseService.API/ERP.PurchaseService.API.csproj", "Services/PurchaseService/ERP.PurchaseService.API/"]
COPY ["ERP.Orchestrator.ServiceDefaults/ERP.Orchestrator.ServiceDefaults.csproj", "ERP.Orchestrator.ServiceDefaults/"]
COPY ["Services/PurchaseService/ERP.PurchaseService.Application/ERP.PurchaseService.Application.csproj", "Services/PurchaseService/ERP.PurchaseService.Application/"]
COPY ["ERP.Shared/ERP.Shared.csproj", "ERP.Shared/"]
COPY ["Services/PurchaseService/ERP.PurchaseService.Infrastructure/ERP.PurchaseService.Infrastructure.csproj", "Services/PurchaseService/ERP.PurchaseService.Infrastructure/"]
RUN dotnet restore "./Services/PurchaseService/ERP.PurchaseService.API/ERP.PurchaseService.API.csproj"
COPY . .
WORKDIR "/src/Services/PurchaseService/ERP.PurchaseService.API"
RUN dotnet build "./ERP.PurchaseService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERP.PurchaseService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERP.PurchaseService.API.dll"]