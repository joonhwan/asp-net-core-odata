using System.Text.Json;
using System.Text.Json.Serialization;
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
using Microsoft.AspNetCore.ResponseCompression;

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
            // EF Core 를 위한 Custom Service 등록 코드
            var connectionString = "Data Source=air-vinyl.db";
            services.AddAirVinylSqliteDb(connectionString);
            
            // Controller 등록시 AddControllers()가 반환하는 IMvcBuilder 객체를 사용해
            // OData 라이브러리를 초기화. 
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                // @ OData
                .AddOData(option =>
                {
                    // 이전 버젼의 다음 코드는
                    //
                    //    option.Count().Filter().Expand().Select().OrderBy().SkipToken().SetMaxTop(1000);
                    // 
                    // 다음 한줄로 OK
                    option.EnableQueryFeatures(maxTopValue: 1000);
                    
                    // OData와 관련한 Routing은 모두 http://localhost:5001/odata/v1/... 로 시작하며, 
                    // 각 Routing별 사용할 OData의 Entity Data Model(=EDM) 을 생성하여 등록함. 
                    // 
                    // AddRouteComponents() 메소드를 version 별로 여러개를 서로 다른 EDM 에 대해 호출가능. 
                    //      
                    option.AddRouteComponents(routePrefix: "odata/v1", AirVinylEntityDataModel.GetEdmModel());
                });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
                
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "AirVinyl.ApiService", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseMiddleware<RequestResponseLoggingMiddleware>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirVinyl.ApiService v1"));
                
            }

            app.UseHttpsRedirection();
            
            app.UseResponseCompression();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}