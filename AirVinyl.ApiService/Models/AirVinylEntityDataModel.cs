using AirVinyl.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace AirVinyl.ApiService.Controllers
{
    // @ OData
    public class AirVinylEntityDataModel
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            // 단수 type -> 복수형 Set이름
            builder.EntitySet<Person>(name: "People");
            builder.EntitySet<VinylRecord>(name: "VinylRecords");
            builder.EntitySet<RecordStore>(name: "RecordStores");
            
            return builder.GetEdmModel();
        }
    }
}