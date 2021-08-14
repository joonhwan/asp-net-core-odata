
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
            // serviceCollection.TryAdd(
            //     new ServiceDescriptor(
            //         typeof(DbContextOptions<AirVinylDbContextBase>),
            //         p =>
            //         {
            //             return CreateDbContextOptions<AirVinylDbContextBase>(p, (p, b) => optionsAction?.Invoke(b));
            //         },
            //         ServiceLifetime.Scoped));
            
            // context 가 base class를 가지는 이런경우에는 AddDbContext<TBase, TContext>()... 형태를..
            serviceCollection.AddDbContext<AirVinylDbContext>(options =>
            {
                options.UseSqlite(connectionString);
                options.EnableSensitiveDataLogging();
            });
            // serviceCollection.AddScoped<AirVinylDbContextBase>(provider =>
            //     provider.GetRequiredService<AirVinylDbContext>());
            return serviceCollection;
        }
        
        private static void ConfigureSqlite(DbContextOptionsBuilder options)
        {
            string postgresSqlConnectionString = "Host=localhost;Database=PeopleDatabase;Username=postgres;Password=example";

            options.UseSqlite(postgresSqlConnectionString);
            options.EnableSensitiveDataLogging();
        }
        
        
        private static DbContextOptions<TContext> CreateDbContextOptions<TContext>(
            IServiceProvider applicationServiceProvider,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
            where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>(
                new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));

            builder.UseApplicationServiceProvider(applicationServiceProvider);

            optionsAction?.Invoke(applicationServiceProvider, builder);

            return builder.Options;
        }
    }
}