using AirVinyl.ApiService.Controllers;
using AirVinyl.DataAccess;
using AirVinyl.DataAccess.Sqlite;
using AirVinyl.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OData;

namespace AirVinyl.ApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Data Source=air-vinyl.db";
            services.AddAirVinylSqliteDb(connectionString);
            // services.AddDbContext<AirVinylDbContextBase>();
            // services.AddDbContext<AirVinylDbContext>(options =>
            // {
            //     options.UseSqlite(connectionString);
            //     // options.EnableSensitiveDataLogging();
            // }); 
            
            services
                .AddControllers()
                // @ OData
                .AddOData(option =>
                {
                    option.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5);
                    
                    // OData의 기본 Routing Rule을 따르지 않고, 명시적으로 Controller에 
                    // [Route("odata/v1/people")] 같은 Attribute로 Routing하게 되면, 
                    // 아래와 같은 기본 route prefix 는 동작하지 않는다. 
                    // 그냥 아래처럼 하지 말고, 각 ODataController 파생클래스별로 [Route(...)] 
                    // 해주는것이 좋다. 
                    
                    option.AddRouteComponents(routePrefix: "odata/v1", AirVinylEntityDataModel.GetEdmModel());
                    // option.AddRouteComponents(AirVinylEntityDataModel.GetEdmModel());
                });
                
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "AirVinyl.ApiService", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirVinyl.ApiService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}