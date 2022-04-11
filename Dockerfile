FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app


# Copy the main source project files
COPY . .

RUN dotnet restore
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM public.ecr.aws/lambda/dotnet:6 AS base
WORKDIR /app
COPY --from=publish /app .
CMD [ "Weather.Api::Weather.Api.LambdaEntryPoint::FunctionHandlerAsync" ]