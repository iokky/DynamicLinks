#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DynamicLinks/DynamicLinks.csproj", "DynamicLinks/"]
COPY ["DynamicLinks.Domain/DynamicLinks.Domain.csproj", "DynamicLinks.Domain/"]
COPY ["DynamicLinks.Services/DynamicLinks.Services.csproj", "DynamicLinks.Services/"]
RUN dotnet restore "./DynamicLinks/DynamicLinks.csproj"
COPY . .
WORKDIR "/src/DynamicLinks"
RUN dotnet build "./DynamicLinks.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DynamicLinks.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicLinks.dll"]