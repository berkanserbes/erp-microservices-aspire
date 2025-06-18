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
COPY ["Services/ProductService/ERP.ProductService.API/ERP.ProductService.API.csproj", "Services/ProductService/ERP.ProductService.API/"]
COPY ["ERP.Orchestrator.ServiceDefaults/ERP.Orchestrator.ServiceDefaults.csproj", "ERP.Orchestrator.ServiceDefaults/"]
COPY ["Services/ProductService/ERP.ProductService.Application/ERP.ProductService.Application.csproj", "Services/ProductService/ERP.ProductService.Application/"]
COPY ["ERP.Shared/ERP.Shared.csproj", "ERP.Shared/"]
COPY ["Services/ProductService/ERP.ProductService.Infrastructure/ERP.ProductService.Infrastructure/ERP.ProductService.Infrastructure.csproj", "Services/ProductService/ERP.ProductService.Infrastructure/ERP.ProductService.Infrastructure/"]
RUN dotnet restore "./Services/ProductService/ERP.ProductService.API/ERP.ProductService.API.csproj"
COPY . .
WORKDIR "/src/Services/ProductService/ERP.ProductService.API"
RUN dotnet build "./ERP.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERP.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERP.ProductService.API.dll"]