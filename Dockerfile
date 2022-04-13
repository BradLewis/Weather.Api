FROM public.ecr.aws/lambda/dotnet:6 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /source
COPY . .
RUN dotnet restore

RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /var/task
COPY --from=build /app/publish .
CMD [ "Weather.Api::Weather.Api.LambdaEntryPoint::FunctionHandlerAsync" ]