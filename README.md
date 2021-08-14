# AirVinyl 샘플 서비스 


## EF Core Migration 관련

이 프로젝트의 구성은 통상 예제에서 하듯이, 한 프로젝트에 모든걸 다 때려 넣은
식이 아니므로, Migration 생성시에는 아래처럼 해야 함.   

```
# migration 생성시 :  "InitialMigration" 으로 migration이 하나 생성됨. 
> dotnet ef migrations add InitialMigration  -p AirVinyl.DataAccess.Sqlite/AirVinyl.DataAccess.Sqlite.csproj -s AirVinyl.ApiService/AirVinyl.ApiService.csproj

# database 에 migration 적용시 
> dotnet ef migrations add InitialMigration  -p AirVinyl.DataAccess.Sqlite/AirVinyl.DataAccess.Sqlite.csproj -s AirVinyl.ApiService/AirVinyl.ApiService.csproj
```

위에서

- `-p`(또는 `--project`) 옵션은 실제 migration 혹은 dbcontext가 존재하는 프로젝트 
- `-s`(또는 `--startup-project`) 옵션은 위 `-p` 옵션의 프로젝트가 사용되는 API 서비스 프로젝트

이다. 


