FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /opt
COPY src/ ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish Din.Application.WebAPI/ -c Release -o app

FROM build AS test
COPY . .
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory:../../reports"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /opt/Din.Application.WebAPI/app .
ENTRYPOINT ["dotnet", "Din.Application.WebAPI.dll"]
