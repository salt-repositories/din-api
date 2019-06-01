FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /opt
COPY ["Din.Infrastructure.Migrations.csproj", "."]
RUN dotnet restore "Din.Infrastructure.Migrations.csproj"
COPY . .
RUN dotnet publish "Din.Infrastructure.Migrations.csproj" -c Release -o /app

FROM microsoft/dotnet:2.2-sdk
WORKDIR /app
COPY --from=build /app .
RUN dotnet tool install -g FluentMigrator.DotNet.Cli