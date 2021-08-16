using System.Linq;
using AirVinyl.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AirVinyl.ApiService.Controllers.OData.V1
{
    [Route("odata/v1")]
    public class RecordStoresController : ODataController
    {
        private readonly AirVinylDbContextBase _dbContext;

        public RecordStoresController(AirVinylDbContextBase dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery]
        [HttpGet("RecordStores")]
        public IActionResult Get()
        {
            return Ok(_dbContext.RecordStores);
        }

        [HttpGet("RecordStores({key})/Tags")]
        [EnableQuery]
        public IActionResult GetRecordStoreTags(int key)
        {
            var recordStore = _dbContext.RecordStores.FirstOrDefault();
            if (recordStore == null)
            {
                return NotFound();
            }

            return Ok(recordStore.Tags);
        }
    }
}