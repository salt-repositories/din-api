FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /opt
COPY src/ ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish Din.Application.WebAPI/ -c Release -o app

FROM build AS test
COPY . .
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory:/opt/reports", "/p:CollectCoverage=true", "/p:CoverletOutput=/opt/reports/", "/p:CoverletOutputFormat=cobertura"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /opt/app .
ENTRYPOINT ["dotnet", "Din.Application.WebAPI.dll"]
