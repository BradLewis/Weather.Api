FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app


# Copy the main source project files
COPY . .

RUN dotnet restore
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Weather.Api.dll"]