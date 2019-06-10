FROM microsoft/dotnet:2.2-sdk AS build
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
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory:../../reports"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /opt/Din.Application.WebAPI/app .
ENTRYPOINT ["dotnet", "Din.Application.WebAPI.dll"]
