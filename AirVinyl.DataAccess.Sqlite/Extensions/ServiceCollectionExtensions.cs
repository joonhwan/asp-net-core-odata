
// ReSharper disable once CheckNamespace

using System;
using System.Collections.Generic;
using AirVinyl.DataAccess;
using AirVinyl.DataAccess.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    // DI 를 이용하여 초기화 루틴을 각 ClassLibrary Specific하게 구현한다.
    //   --> Startup.cs 같은 곳에서
    //    
    //      services.AddAirVinylSqliteDb("Data Source=air-vinyl.db");
    // 
    //  처럼 하고, 복잡한 초기화는 아래 구현에서 수행. 
    // 
    // hint from :
    // https://github.com/dotnet/efcore/blob/4f50f66ced9c30ddc82d7ddcc3b9c541589f985f/src/EFCore.Sqlite.Core/Extensions/SqliteServiceCollectionExtensions.cs
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAirVinylSqliteDb(
            this IServiceCollection serviceCollection,
            string connectionString,
            Action<DbContextOptionsBuilder> optionsAction = null,
            Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null
        )
        {
            // context 가 base class를 가지는 이런경우에는 AddDbContext<TBase, TContext>()... 형태를..
            serviceCollection.AddDbContext<AirVinylDbContextBase, AirVinylDbContext>(options =>
            {
                options.UseSqlite(connectionString);
                options.EnableSensitiveDataLogging();
            });
            
            return serviceCollection;
        }
    }
}