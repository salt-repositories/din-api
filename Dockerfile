FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /opt
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
COPY src/*.sln ./
RUN dotnet restore
COPY src/ ./
RUN dotnet build
RUN dotnet publish Din.Application.WebAPI/ -c Release -o app

FROM build AS test
COPY . .
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory:/opt/reports", "/p:CollectCoverage=true", "/p:CoverletOutput=/opt/reports/", "/p:CoverletOutputFormat=cobertura"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /opt/Din.Application.WebAPI/app .
ENTRYPOINT ["dotnet", "Din.Application.WebAPI.dll"]
