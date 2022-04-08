FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app


# Copy the main source project files
COPY . .

RUN dotnet restore
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Weather.Api.dll"]