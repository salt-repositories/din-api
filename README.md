# Din API

[![dotnet](https://img.shields.io/badge/.NET%20Core-2.2-blueviolet.svg?logo=.net)](https://docs.microsoft.com/en-us/dotnet/core/)
[![Build Status](https://jenkins.naebers.me/buildStatus/icon?job=din-api/master)](https://jenkins.naebers.me/job/din-api/job/master/) [![Vault](https://img.shields.io/badge/vault-secured-lightgrey.svg?colorB=7c8797&colorA=000000&logo=data:image/svg%2bxml;base64,PHN2ZyB3aWR0aD0iMjIiIGhlaWdodD0iMjIiIHZpZXdCb3g9IjAgMCAyMiAyMiIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48dGl0bGU+VmF1bHQgTG9nbzwvdGl0bGU+PHBhdGggZD0iTTAgMGwxMC42OTIgMjEuNDYyTDIxLjQ2MiAwSDB6bTExLjk2NCA1LjU1aDEuMjQ0VjQuMzA4aC0xLjI0NHYxLjI0NHptLTEuODU1IDBoMS4yNDNWNC4zMDhIMTAuMTF2MS4yNDR6bS0xLjg2NiAwaDEuMjQ0VjQuMzA4SDguMjQzdjEuMjQ0em0zLjcyMSAxLjg2N2gxLjI0NFY2LjE3NGgtMS4yNDR2MS4yNDN6bS0xLjg1NSAwaDEuMjQzVjYuMTc0SDEwLjExdjEuMjQzem0tMS44NjYgMGgxLjI0NFY2LjE3NEg4LjI0M3YxLjI0M3ptMy43MzIgMS44NjdoMS4yNDNWOC4wNGgtMS4yNDN2MS4yNDR6bS0xLjg2NiAwaDEuMjQzVjguMDRIMTAuMTF2MS4yNDR6bS0xLjg2NiAwaDEuMjQ0VjguMDRIOC4yNDN2MS4yNDR6bTEuODY2IDEuODY2aDEuMjQzVjkuOTA2SDEwLjExdjEuMjQ0eiIgZmlsbD0iI0ZGRiIgZmlsbC1ydWxlPSJldmVub2RkIi8+PC9zdmc+)](https://vault.naebers.me) 

Developing din-api
--------------------

If you wish to work on the din-api project, you'll
first need the [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download) installed on your machine.

**Required setup commands**

```sh
dotnet restore
dotnet build
```

**Secrets**

All secrets used within the din-api project are stored in vault. To access these secrets either a github access token or application id-secret pair is needed. The application will decide on runtime which one to use depending on the environment.

**Database**

This project makes use of the [FluentMigrator](https://github.com/fluentmigrator/fluentmigrator) package.

A local database instance is needed for develment. The connection string to connect to this database instance is stored within the `application.json` file. The local database instance can be iniated using the commands provided by the `FluentMigrator CLI Tool`.

*Instalation*
```sh
Install following: dotnet tool install -g FluentMigrator.DotNet.Cli
```

*Migrate*
```sh
dotnet fm migrate -p mysql -c "connection string" -a "migration assembly"
```

*Rollback*
```sh
dotnet fm rollback -p mysql -c "connection string" -a "migration assembly"
```

**Environment variables**

|**Name**|**Value**|**Environment**|**Description**|
|--------|---------|---------------|---------------|
|VAULT_URL|https://vault.naebers.me|All|The url to the secret management system (vault).|
|VAULT_GITHUB_AT|`hex-string`|Development|The developers github access token to access the vault secrets.|
|VAULT_ID|`hex-string`|Production|The application ID to access the vault secrets.|
|VAULT_SECRET|`hex-string`|Production|The application SECRET to access the vault secrets.|