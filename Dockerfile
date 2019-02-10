FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY src/ ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish Din.Application.WebAPI/ -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /app/Din.Application.WebAPI/out .
ENTRYPOINT ["dotnet", "Din.Application.WebAPI.dll"]
