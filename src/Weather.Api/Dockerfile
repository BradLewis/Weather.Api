FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Weather.Api/Weather.Api.csproj", "Weather.Api/"]
RUN dotnet restore "Weather.Api/Weather.Api.csproj"
COPY . .
WORKDIR "/src/Weather.Api"
RUN dotnet build "Weather.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather.Api.dll"]