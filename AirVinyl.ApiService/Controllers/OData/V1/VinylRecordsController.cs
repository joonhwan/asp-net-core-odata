using System.Linq;
using System.Threading.Tasks;
using AirVinyl.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.ApiService.Controllers.OData.V1
{
    public class VinylRecordsController : ODataController
    {
        private readonly AirVinylDbContextBase _dbContext;

        public VinylRecordsController(AirVinylDbContextBase dbContext)
        {
            _dbContext = dbContext;
        }
        
        // 이전 버젼에서는...
        //
        // [HttpGet]
        // [ODataRoute("VinylRecords")] 
        //
        // 이런식으로 했었음.
        // 
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.VinylRecords);
        }
        
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get(int key)
        {
            var found = await _dbContext.VinylRecords.FirstOrDefaultAsync(r => r.VinylRecordId == key);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
    }
}