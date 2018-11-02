#pull dotnet build image
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

#resore packages
COPY src/Din/*.csproj ./Din/
COPY src/Din.Data/*.csproj ./Din.Data/
COPY src/Din.Service/*.csproj ./Din.Service/
COPY src/Din.Tests/*.csproj ./Din.Tests/
COPY src/Din.sln ./
RUN dotnet restore

#run tests
COPY src/ ./
RUN dotnet test ./Din.Tests/
