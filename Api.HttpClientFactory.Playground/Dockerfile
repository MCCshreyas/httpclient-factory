#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Api.HttpClientFactory.Playground/Api.HttpClientFactory.Playground.csproj", "Api.HttpClientFactory.Playground/"]
RUN dotnet restore "Api.HttpClientFactory.Playground/Api.HttpClientFactory.Playground.csproj"
COPY . .
WORKDIR "/src/Api.HttpClientFactory.Playground"
RUN dotnet build "Api.HttpClientFactory.Playground.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.HttpClientFactory.Playground.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.HttpClientFactory.Playground.dll"]