FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY src/ ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish Din/ -c Release -o out

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /app/Din/out .
ENTRYPOINT ["dotnet", "Din.dll"]
