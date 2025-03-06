FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5198

ENV ASPNETCORE_URLS=http://+:5198

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["payment-control-api/payment-control-api.csproj", "payment-control-api/"]
RUN dotnet restore "payment-control-api/payment-control-api.csproj"
COPY . .
WORKDIR "/src/payment-control-api"
RUN dotnet build "payment-control-api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "payment-control-api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "payment-control-api.dll"]
