# Din API

[![dotnet](https://img.shields.io/badge/6-blueviolet.svg?logo=.net)](https://docs.microsoft.com/en-us/dotnet/core/) [![Build Status](https://jenkins.naebers.net/buildStatus/icon?job=Salt+Repositories%2Fdin-api%2Fmaster)](https://jenkins.naebers.net/job/Salt%20Repositories/job/din-api/job/master/) [![Vault](https://img.shields.io/badge/vault-secured-lightgrey.svg?colorB=7c8797&colorA=000000&logo=data:image/svg%2bxml;base64,PHN2ZyB3aWR0aD0iMjIiIGhlaWdodD0iMjIiIHZpZXdCb3g9IjAgMCAyMiAyMiIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48dGl0bGU+VmF1bHQgTG9nbzwvdGl0bGU+PHBhdGggZD0iTTAgMGwxMC42OTIgMjEuNDYyTDIxLjQ2MiAwSDB6bTExLjk2NCA1LjU1aDEuMjQ0VjQuMzA4aC0xLjI0NHYxLjI0NHptLTEuODU1IDBoMS4yNDNWNC4zMDhIMTAuMTF2MS4yNDR6bS0xLjg2NiAwaDEuMjQ0VjQuMzA4SDguMjQzdjEuMjQ0em0zLjcyMSAxLjg2N2gxLjI0NFY2LjE3NGgtMS4yNDR2MS4yNDN6bS0xLjg1NSAwaDEuMjQzVjYuMTc0SDEwLjExdjEuMjQzem0tMS44NjYgMGgxLjI0NFY2LjE3NEg4LjI0M3YxLjI0M3ptMy43MzIgMS44NjdoMS4yNDNWOC4wNGgtMS4yNDN2MS4yNDR6bS0xLjg2NiAwaDEuMjQzVjguMDRIMTAuMTF2MS4yNDR6bS0xLjg2NiAwaDEuMjQ0VjguMDRIOC4yNDN2MS4yNDR6bTEuODY2IDEuODY2aDEuMjQzVjkuOTA2SDEwLjExdjEuMjQ0eiIgZmlsbD0iI0ZGRiIgZmlsbC1ydWxlPSJldmVub2RkIi8+PC9zdmc+)](https://vault.naebers.net) 

Developing din-api
--------------------

If you wish to work on the din-api project, you'll
first need the [.NET 6 SDK](https://dotnet.microsoft.com/download) installed on your machine.

**Required setup commands**

```sh
dotnet restore
dotnet build
```

**Secrets**

All secrets used within the din-api project are stored in vault. To access these secrets either a github access token or application id-secret pair is needed. The application will decide on runtime which one to use depending on the environment.

**Environment variables**

|**Name**|**Value**|**Environment**|**Description**|
|--------|---------|---------------|---------------|
|VAULT_URL|https://vault.naebers.net|All|The url to the secret management system (vault).|
|VAULT_GITHUB_AT|`string`|Development|The developers github access token to access the vault secrets.|
|VAULT_ID|`string`|Production|The application ID to access the vault secrets.|
|VAULT_SECRET|`string`|Production|The application SECRET to access the vault secrets.|
