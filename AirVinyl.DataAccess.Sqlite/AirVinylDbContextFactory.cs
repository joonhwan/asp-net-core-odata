using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace AirVinyl.DataAccess.Sqlite
{
    // // migration 생성등 dotnet ef 명령을 실행할 때는 항상 DB접속이 되는 환경이 필요하다.
    // // 이것이 실제로 서비스가 사용할 DB와 다른 경우에는 아래와 같은 "디자인타임 DbContext 생성"에 
    // // 대한 코드가 필요하다. 
    // //
    // // https://docs.microsoft.com/ko-kr/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    // //
    // // 하지만, 통상의 경우, 실제 테스트할 서비스가 사용할 DB가 미리 만들어져 있어야 하는 경우가 
    // // 대부분이므로, 아직 적절한 Use Case는 모르겠음. 
    // // 
    // public class AirVinylDbContextFactory : IDesignTimeDbContextFactory<AirVinylDbContext>
    // {
    //     public AirVinylDbContext CreateDbContext(string[] args)
    //     {
    //         var optionsBuilder = new DbContextOptionsBuilder<AirVinylDbContext>();
    //         optionsBuilder.UseSqlite("Data Source=design-time-air-vinyl.db");
    //         return new AirVinylDbContext(optionsBuilder.Options);
    //     }
    // }
}