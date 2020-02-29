FROM mcr.microsoft.com/dotnet/core/sdk:2.1-alpine AS build
WORKDIR /opt
COPY ["Din.Infrastructure.Migrations.csproj", "."]
RUN dotnet restore "Din.Infrastructure.Migrations.csproj"
COPY . .
RUN dotnet publish "Din.Infrastructure.Migrations.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-alpine
WORKDIR /app
COPY --from=build /app .
RUN dotnet tool install -g FluentMigrator.DotNet.Cli
ENV PATH="/root/.dotnet/tools:${PATH}"
